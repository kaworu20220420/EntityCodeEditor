
using Microsoft.VisualBasic.FileIO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Windows.Forms.LinkLabel;

namespace EntityCodeEditor
{
	internal class CommentGenerator
	{
		internal void AddComment(string filePath)
		{
			var lines = File.ReadAllLines(filePath, Encoding.UTF8).ToList();
			var updatedContent = AddComments(lines);

			FileSystem.DeleteFile(filePath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
			File.WriteAllText(filePath, updatedContent);
			Console.WriteLine("コメント追加完了");
		}

		static string AddComments(List<string> lines)
		{
			// 逆順でコメント挿入
			for (int i = lines.Count - 1; i >= 0; i--)
			{
				var line = lines[i];
				if (!lines[i].Contains("public"))
					continue;

				var indentMatch = Regex.Match(lines[i], @"^(\s*)");
				var indent = indentMatch.Success ? indentMatch.Groups[1].Value : "";

				// プロパティ判定（型名とプロパティ名を抽出）
				var propMatch = Regex.Match(line.Trim(), @"^public\s+([\w<>,\s]+)\s+(\w+)\s*\{.*\}");
				if (propMatch.Success)
				{
					var typeName = propMatch.Groups[1].Value.Trim();
					var propName = propMatch.Groups[2].Value.Trim();
					var comment = $"{indent}/// <summary>\r\n{indent}/// {propName} ({typeName})\r\n{indent}/// </summary>";
					if (i + 1 < lines.Count && lines[i + 1].Contains("/summary"))
					{
						continue;
					}
					lines[i] = $"{comment}\r\n{lines[i]}";
					continue;
				}

				// メソッド判定（戻り値・メソッド名・引数を抽出）
				var methodMatch = Regex.Match(lines[i].Trim(), @"^public\s+([\w<>,\s]+)\s+(\w+)\s*\(([^)]*)\)");
				if (methodMatch.Success)
				{
					var returnType = methodMatch.Groups[1].Value.Trim();
					var methodName = methodMatch.Groups[2].Value;
					var args = methodMatch.Groups[3].Value;

					var commentLines = new List<string>
					{
						$"{indent}/// <summary>",
						$"{indent}/// {methodName}",
						$"{indent}/// </summary>"
					};

					if (!string.IsNullOrWhiteSpace(args))
					{
						foreach (var arg in args.Split(','))
						{
							var parts = arg.Trim().Split(' ');
							if (parts.Length >= 2)
							{
								var paramName = parts[^1];
								commentLines.Add($"{indent}/// <param name=\"{paramName}\">{paramName}</param>");
							}
						}
					}

					if (returnType != "void")
					{
						commentLines.Add($"{indent}/// <returns>{returnType}</returns>");
					}

					if (i + 1 < lines.Count && lines[i + 1].Contains("/summary"))
					{
						continue;
					}
					lines[i] = string.Join("\r\n", commentLines) + lines[i];
					continue;
				}
			}

			return string.Join("\r\n", lines);
		}
	}
}