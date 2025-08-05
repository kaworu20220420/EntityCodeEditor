
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
				var updatedLines = new List<string>(lines);

				for (int i = lines.Count - 1; i >= 0; i--)
				{
					var line = lines[i].Trim();

					// public プロパティ（1行のみ）
					var propMatch = Regex.Match(line, @"^public\s+\w+\s+(\w+)\s*\{.*\}");
					if (propMatch.Success)
					{
						var propName = propMatch.Groups[1].Value;
						if (i == 0 || !lines[i - 1].Trim().StartsWith("/// <summary>"))
						{
							updatedLines.Insert(i, $"    /// <summary>\r\n    /// {propName}\r\n    /// </summary>");
						}
						continue;
					}

					// public メソッド（1行のみ）
					var methodMatch = Regex.Match(line, @"^public\s+(\w+)\s+(\w+)\s*\(([^)]*)\)");
					if (methodMatch.Success)
					{
						var returnType = methodMatch.Groups[1].Value;
						var methodName = methodMatch.Groups[2].Value;
						var args = methodMatch.Groups[3].Value;

						if (i == 0 || !lines[i - 1].Trim().StartsWith("/// <summary>"))
						{
							var commentLines = new List<string>
					{
						$"    /// <summary>",
						$"    /// {methodName}",
						$"    /// </summary>"
					};

							if (!string.IsNullOrWhiteSpace(args))
							{
								foreach (var arg in args.Split(','))
								{
									var parts = arg.Trim().Split(' ');
									if (parts.Length >= 2)
									{
										var paramName = parts[^1];
										commentLines.Add($"    /// <param name=\"{paramName}\">{paramName}</param>");
									}
								}
							}

							if (returnType != "void")
							{
								commentLines.Add($"    /// <returns>{returnType}</returns>");
							}

							updatedLines.Insert(i, string.Join("\r\n", commentLines));
						}
					}
				}

				return string.Join("\r\n", updatedLines);	
		}
	}
}
