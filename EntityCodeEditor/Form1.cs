namespace EntityCodeEditor
{
	public partial class EntityCodeEditorForm : Form
	{
		private AnnotationProcessor annotationProcessor = new AnnotationProcessor();
		private InterfaceRepositoryGenerator generator = new InterfaceRepositoryGenerator();
		private OutputPathGenerator outputPathGenerator = new OutputPathGenerator();
		string addUsing = "using System.ComponentModel.DataAnnotations;";
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

		private void ExecuteButton_Click(object sender, EventArgs e) =>
			(Directory.Exists(FilePathBox.Text) ? Directory.GetFiles(FilePathBox.Text) :
			File.Exists(FilePathBox.Text) ? new[] { FilePathBox.Text } : Array.Empty<string>()).ToList()
				.ForEach(file => CodeEdit(file));

		private void CodeEdit(string file)
		{
			var result = annotationProcessor.AddAnnotationOperation(file, TimeStampRadioButton.Checked, ToTrashRadioButton.Checked, MoveOldFolderRadioButton.Checked);
			var outputPath = outputPathGenerator.GenerateOutputPath(FilePathBox.Text);
			(string NamespaceName, string ClassName) = generator.GenerateInterface(result, outputPath);
			generator.GenerateRepository(NamespaceName, ClassName, DbContextTextBox.Text, outputPath);
		}
	}
}