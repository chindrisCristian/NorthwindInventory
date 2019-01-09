using GalaSoft.MvvmLight;

namespace NorthwindInventory.ViewModel
{
	/// <summary>
	/// The view model for the <see cref="FirstPage"/>.
	/// </summary>
	public class FirstViewModel : ViewModelBase
    {
		#region Constructor

		public FirstViewModel()
		{
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The title of this project.
		/// </summary>
		private string _projectTitle = "Northwind Inventory";
		public string ProjectTitle
		{
			get => _projectTitle;
			set => Set(ref _projectTitle, value);
		}

		/// <summary>
		/// The course name.
		/// </summary>
		private string _courseTitle = "Proiectarea bazelor de date";
		public string CourseTitle
		{
			get => _courseTitle;
			set => Set(ref _courseTitle, value);
		}
		#endregion
	}

}