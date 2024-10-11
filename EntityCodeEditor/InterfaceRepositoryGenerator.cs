using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;

namespace EntityCodeEditor
{
	public class InterfaceRepositoryGenerator
	{
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

		private static void WriteFile(string outputPath, string content, string fileName)
		{
			var fullPath = Path.Combine(outputPath, fileName);

			// 元のファイルをゴミ箱に移動
			if (File.Exists(fullPath))
			{
				FileSystem.DeleteFile(fullPath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
			}

			// ファイルに書き込む
			File.WriteAllText(fullPath, content);
		}

		public void GenerateRepository(string namespaceName, string className, string dbContext, string outputPath)
		{
			// リポジトリクラスの内容を生成
			var repositoryContent = $@"using Microsoft.EntityFrameworkCore;

namespace {namespaceName}
{{
    public class {className}Repository : I{className}Repository
    {{
        private readonly {dbContext} _context;

        public {className}Repository({dbContext} context)
        {{
            _context = context;
        }}

        public async Task<IEnumerable<{className}>> GetAllAsync()
        {{
            return await _context.{className}s.ToListAsync();
        }}
    }}
}}";

			// ファイルパスを生成
			var fileName = $"{className}Repository.cs";
			WriteFile(outputPath, repositoryContent , fileName);
		}
	}
}
