
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
			// コメント対象行を抽出（public を含み、直前に </summary> がない）
			var targets = lines
				.Select((line, i) => new { line, i })
				.Where(x =>
					x.line.Contains("public") &&
					!x.line.Contains("///") &&
					(x.i == 0 || !lines[x.i - 1].Trim().Contains("</summary>"))
				)
				.ToList();

			// 逆順でコメント挿入
			for (int i = targets.Count - 1; i >= 0; i--)
			{
				var target = targets[i];
				var line = target.line;
				var index = target.i;

				var indentMatch = Regex.Match(line, @"^(\s*)");
				var indent = indentMatch.Success ? indentMatch.Groups[1].Value : "";

				var trimmed = line.Trim();

				// プロパティ判定（型名とプロパティ名を抽出）
				var propMatch = Regex.Match(trimmed, @"^public\s+([\w<>,\s]+)\s+(\w+)\s*\{.*\}");
				if (propMatch.Success)
				{
					var typeName = propMatch.Groups[1].Value.Trim();
					var propName = propMatch.Groups[2].Value;
					var comment = $"{indent}/// <summary>\r\n{indent}/// {propName} ({typeName})\r\n{indent}/// </summary>";
					lines.Insert(index, comment);
					continue;
				}

				// メソッド判定（戻り値・メソッド名・引数を抽出）
				var methodMatch = Regex.Match(trimmed, @"^public\s+([\w<>,\s]+)\s+(\w+)\s*\(([^)]*)\)");
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

					lines.Insert(index, string.Join("\r\n", commentLines));
				}
			}

			return string.Join("\r\n", lines);
		}
	}
}