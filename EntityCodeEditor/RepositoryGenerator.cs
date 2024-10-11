namespace EntityCodeEditor
{
	public class RepositoryGenerator : InterfaceGenerator
	{
		public string GenerateRepository(string namespaceName, string className, string dbContext, string outputPath)
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
			return className + "Repository";
		}
	}
}
