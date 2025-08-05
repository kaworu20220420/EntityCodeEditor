
using Microsoft.VisualBasic.FileIO;
using System.Text;
using System.Text.RegularExpressions;

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
			Console.WriteLine("コメント追加完了。先輩のコード、さらにイケてる感じになったよ。");
		}

		static string AddComments(List<string> lines)
		{
			for (int i = lines.Count - 1; i >= 0; i--)
			{
				var line = lines[i];
				var trimmed = line.Trim();

				// インデント取得
				var indentMatch = Regex.Match(line, @"^(\s*)");
				var indent = indentMatch.Success ? indentMatch.Groups[1].Value : "";

				// 直前3行以内に summary コメントがあるか確認
				bool hasSummary = false;
				for (int j = i - 1; j >= Math.Max(0, i - 3); j--)
				{
					if (lines[j].Trim().Contains("</summary>"))
					{
						hasSummary = true;
						break;
					}
				}
				if (hasSummary) continue;

				// プロパティ判定（1行のみ）
				var propMatch = Regex.Match(trimmed, @"^public\s+\w+\s+(\w+)\s*\{.*\}");
				if (propMatch.Success)
				{
					var propName = propMatch.Groups[1].Value;
					var comment = $"{indent}/// <summary>\r\n{indent}/// {propName}\r\n{indent}/// </summary>";
					lines.Insert(i, comment);
					continue;
				}

				// メソッド判定（1行のみ）
				var methodMatch = Regex.Match(trimmed, @"^public\s+(\w+)\s+(\w+)\s*\(([^)]*)\)");
				if (methodMatch.Success)
				{
					var returnType = methodMatch.Groups[1].Value;
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

					lines.Insert(i, string.Join("\r\n", commentLines));
				}
			}

			return string.Join("\r\n", lines);
		}
	}
}