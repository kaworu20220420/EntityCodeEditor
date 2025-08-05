
using Microsoft.VisualBasic.FileIO;
using System.Text;
using System.Text.RegularExpressions;

namespace EntityCodeEditor
{
	internal class CommentGenerator
	{
		internal void AddComment(string filePath)
		{
			var originalContent = File.ReadAllText(filePath, Encoding.UTF8);
			var updatedContent = AddComments(originalContent);

			FileSystem.DeleteFile(filePath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
			File.WriteAllText(filePath, updatedContent);
			Console.WriteLine("コメント追加完了。先輩のコード、さらにイケてる感じになったよ。");
		}

		static string AddComments(string content)
		{
			// プロパティ（空行なし対応＋空行挿入）
			content = Regex.Replace(content, @"(?<!/// <summary>[\s\S]*?)\s*(public\s+\w+\s+\w+\s*\{[^}]*\})", match =>
			{
				var nameMatch = Regex.Match(match.Value, @"public\s+\w+\s+(\w+)\s*{");
				var name = nameMatch.Success ? nameMatch.Groups[1].Value : "Property";
				var summary = $"/// <summary>\r\n    /// {name}\r\n    /// </summary>\r\n\r\n    ";
				return summary + match.Value;
			});

			// メソッド・コンストラクタ（戻り値あり対応）
			content = Regex.Replace(content, @"(?<!/// <summary>[\s\S]*?)\s*(public\s+(\w+)\s+(\w+)\s*\(([^)]*)\)\s*\{)", match =>
			{
				var fullSignature = match.Groups[1].Value;
				var returnType = match.Groups[2].Value;
				var methodName = match.Groups[3].Value;
				var args = match.Groups[4].Value;

				var summary = $"/// <summary>\r\n    /// {methodName}\r\n    /// </summary>\r\n";
				var paramComments = "";

				if (!string.IsNullOrWhiteSpace(args))
				{
					foreach (var arg in args.Split(','))
					{
						var parts = arg.Trim().Split(' ');
						if (parts.Length >= 2)
						{
							var paramName = parts[^1].Trim();
							paramComments += $"/// <param name=\"{paramName}\">{paramName}</param>\r\n";
						}
					}
				}

				var returnsComment = returnType != "void"
					? $"/// <returns>{returnType}</returns>\r\n"
					: "";

				return summary + paramComments + returnsComment + "\r\n    " + fullSignature;
			});

			return content;
		}
	}
}