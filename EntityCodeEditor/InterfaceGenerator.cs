using Microsoft.VisualBasic.FileIO;
using System.Text;
using System.Text.RegularExpressions;

namespace EntityCodeEditor
{
	public class InterfaceGenerator
	{
		/// <summary>
		/// Modelのコンテンツをもとに、Databaseアクセス用のインターフェイスのファイルを作成して保存する。
		/// </summary>
		/// <param name="input">Modelの内容</param>
		/// <param name="outputPath">Intarfaceの出力先のフォルダ</param>
		/// <returns>名前空間、クラス名</returns>
		/// <exception cref="InvalidOperationException"></exception>
		public (string NamespaceName, string ClassName) GenerateInterface(string input, string outputPath)
		{
			// 名前空間を抽出
			var namespaceMatch = Regex.Match(input, @"namespace\s+([\w\.]+)");
			if (!namespaceMatch.Success)
			{
				throw new InvalidOperationException("名前空間が見つかりませんでした。");
			}
			var namespaceName = namespaceMatch.Groups[1].Value;

			// クラス名を抽出
			var classNameMatch = Regex.Match(input, @"public\s+(partial\s+)?class\s+(\w+)");
			if (!classNameMatch.Success)
			{
				throw new InvalidOperationException("クラス名が見つかりませんでした。");
			}
			var className = classNameMatch.Groups[2].Value;

			// インターフェースの内容を生成
			var interfaceContent = $@"
namespace {namespaceName}
{{
    public interface I{className}Repository
    {{
        Task<IEnumerable<{className}>> GetAllAsync();
    }}
}}";

			// ファイルパスを生成
			var fileName = $"I{className}Repository.cs";
			WriteFile(outputPath, interfaceContent, fileName);

			// 名前空間とクラス名をタプルで戻す
			return (namespaceName, className);
		}

		public static void WriteFile(string outputPath, string content, string fileName)
		{
			var fullPath = Path.Combine(outputPath, fileName);

			// 元のファイルをゴミ箱に移動
			if (File.Exists(fullPath))
			{
				FileSystem.DeleteFile(fullPath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
			}

			// ファイルに書き込む
			File.WriteAllText(fullPath, content, Encoding.UTF8);
		}
	}
}
