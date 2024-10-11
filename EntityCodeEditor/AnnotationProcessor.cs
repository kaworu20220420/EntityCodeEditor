using Microsoft.VisualBasic.FileIO;
using System.Text;
using System.Text.RegularExpressions;

namespace EntityCodeEditor
{
	public class AnnotationProcessor
	{
		private string addUsing = "using System.ComponentModel.DataAnnotations;";

		/// <summary>
		/// summaryを元に[Data(name=)]のアノテーションを追加する
		/// </summary>
		/// <param name="fileFullPath">対象のファイルのフルパス</param>
		/// <param name="timeStampChecked">タイムスタンプを追加して新規のファイルを作る</param>
		/// <param name="toTrashChecked">元のファイルをごみ箱に捨てて新規のファイルを元のファイル名で作成する</param>
		/// <param name="moveOldFolderChecked">元のファイルをoldフォルダに移動して新規のファイルを元のファイル名で作成する</param>
		/// <returns>対象のコード内容</returns>
		public string AddAnnotationOperation(string fileFullPath, bool timeStampChecked, bool toTrashChecked, bool moveOldFolderChecked)
		{
			var input = File.ReadAllText(fileFullPath, Encoding.UTF8);
			var output1 = ConvertInputToOutput(input);
			if (input == output1)
			{
				return input;
			}
			var output2 = AddUsingInput(output1);

			var outputPath = timeStampChecked ?
				Path.Combine(Path.GetDirectoryName(fileFullPath), DateTime.Now.ToString("yyyy_MMdd_HHmm") + Path.GetFileName(fileFullPath)) : fileFullPath;
			if (toTrashChecked)
			{
				// 元のファイルをゴミ箱に移動
				FileSystem.DeleteFile(fileFullPath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
			}
			else if (moveOldFolderChecked)
			{
				// oldフォルダのパスを作成
				var oldFolderPath = Path.Combine(Path.GetDirectoryName(fileFullPath), "old");
				// oldフォルダが存在しない場合は作成
				if (!Directory.Exists(oldFolderPath))
				{
					Directory.CreateDirectory(oldFolderPath);
				}
				// 元のファイルをoldフォルダに移動
				File.Move(fileFullPath, Path.Combine(oldFolderPath, Path.GetFileName(fileFullPath)));
			}
			File.AppendAllText(outputPath, output2, Encoding.UTF8);
			return output2;
		}

		private string AddUsingInput(string input) => input.Contains(addUsing) ? input : addUsing + "\r\n" + input;

		public string ConvertInputToOutput(string input)
		{
			string returnTabsString = @"
		";
			string returnString = @"
";

			// プロパティのsummaryタグを置換
			string propertyPattern = @"(\s*/// <summary>\s*\r\n\s*/// (.*?)\s*\r\n\s*/// </summary>\s*\r\n\s*public\s+(\w+\??)\s+(\w+)\s*{ get; set; })";
			string propertyReplacement = @"[return][returnTabs]/// <summary>[returnTabs]/// $2[returnTabs]/// </summary>[returnTabs][Display(Name = ""$2"")][returnTabs]public $3 $4 { get; set; }";

			// プロパティ部分の置換
			return Regex.Replace(input, propertyPattern, propertyReplacement).Replace("[returnTabs]", returnTabsString).Replace("[return]", returnString);
		}
	}
}
