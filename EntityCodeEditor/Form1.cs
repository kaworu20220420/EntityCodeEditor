namespace EntityCodeEditor
{
	public partial class EntityCodeEditorForm : Form
	{
		private AnnotationProcessor annotationProcessor = new AnnotationProcessor();
		private InterfaceGenerator generator = new InterfaceGenerator();
		private RepositoryGenerator repositoryGenerator = new RepositoryGenerator();
		private OutputPathGenerator outputPathGenerator = new OutputPathGenerator();
		private RepositoriesRegister RepositoriesRegister = new RepositoriesRegister();
		private CommentGenerator commentGenerator = new CommentGenerator();

		private List<string> repositoryes = new List<string>();
		string addUsing = "using System.ComponentModel.DataAnnotations;";
		string outputPath = string.Empty;
		public EntityCodeEditorForm() =>InitializeComponent();

		private void FilePathBoxBox_DragEnter(object sender, DragEventArgs e) =>
			e.Effect = e.Data.GetDataPresent(DataFormats.Text) ? DragDropEffects.Copy:
							e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All:
							DragDropEffects.None;

		private void FilePathBoxBox_DragDrop(object sender, DragEventArgs e)
		{
			FilePathBox.Text = e.Data.GetDataPresent(DataFormats.Text) ?
				// ドラッグしたデータをUTF-8として取得
				(string)e.Data.GetData(DataFormats.UnicodeText) :
			e.Data.GetDataPresent(DataFormats.FileDrop) ?
				// ドラッグ＆ドロップしたファイルのパスを取得。最初のファイルのパスを設定
				((string[])e.Data.GetData(DataFormats.FileDrop))?[0] : string.Empty;
			DbContextTextBox.Text=Path.GetFileNameWithoutExtension(FilePathBox.Text) + "dbContext";
		}

		private void ExecuteButton_Click(object sender, EventArgs e)
		{
			if (AddComment.Checked)
			{
				commentGenerator.AddComment(FilePathBox.Text);
				return;
			}

			outputPath = outputPathGenerator.GenerateOutputPath(FilePathBox.Text);
			(Directory.Exists(FilePathBox.Text) ? Directory.GetFiles(FilePathBox.Text) :
			File.Exists(FilePathBox.Text) ? new[] { FilePathBox.Text } : Array.Empty<string>()).ToList()
				.ForEach(file => CodeEdit(file));
			var programCsPath = outputPathGenerator.SetCherckProgramcsPath(FilePathBox.Text);
			if (string.IsNullOrEmpty(programCsPath))
			{
				return;
			}
			RepositoriesRegister.Register(programCsPath, repositoryes);
		}

		private void CodeEdit(string file)
		{
			if (file.ToLower().Contains("dbcontext")) 
			{ 
				return; 
			}

			var result = annotationProcessor.AddAnnotationOperation(file, TimeStampRadioButton.Checked, ToTrashRadioButton.Checked, MoveOldFolderRadioButton.Checked);
			(string NamespaceName, string ClassName) = generator.GenerateInterface(result, outputPath);
			var repositoryName = repositoryGenerator.GenerateRepository(NamespaceName, ClassName, DbContextTextBox.Text, outputPath);
			repositoryes.Add(repositoryName);
		}
	}
}