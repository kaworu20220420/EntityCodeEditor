using System.Text;

namespace EntityCodeEditor
{
	public class RepositoriesRegister : InterfaceGenerator
	{
		internal void Register(string programCsPath, List<string> repositoryes)
		{
			var code= File.ReadAllText(programCsPath, Encoding.UTF8);
			var addLocation = "// InterfaceとRepositoryを登録する\r\n";
			var addText = string.Empty;

			if (repositoryes == null || repositoryes.Count==0)
			{
				var directory = Path.GetDirectoryName(programCsPath);
				var repositoryFolder= Directory.GetDirectories(directory, "Repository", SearchOption.AllDirectories).FirstOrDefault();
				if (repositoryFolder == null) { return; }

				var files = Directory.GetFiles(repositoryFolder, "*Repository.cs");
				foreach (var file in files)
				{
					repositoryes.Add(Path.GetFileNameWithoutExtension(file));
				}
			}
			if (repositoryes.Count == 0) { return; }
			foreach (var repository in repositoryes)
			{
				var addrepository= "builder.Services.AddScoped<I"+repository + ", " + repository + ">();" + "\r\n";
				if (code.Contains(addrepository)) continue;
				addText += addrepository;
			}
			code.Replace(addLocation, addLocation + addText);
			WriteFile(Path.GetDirectoryName(programCsPath), code, Path.GetFileName(programCsPath));
		}
	}
}
