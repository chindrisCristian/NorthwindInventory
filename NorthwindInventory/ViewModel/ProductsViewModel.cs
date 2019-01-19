using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using NorthwindInventory.Helpers;
using NorthwindInventory.Models;
using NorthwindInventory.Models.Services;
using NorthwindInventory.Services;

namespace NorthwindInventory.ViewModel
{
    /// <summary>
    /// The view model for the <see cref="ProductsPage"/>.
    /// </summary>
    public class ProductsViewModel : ViewModelBase
    {
        #region Constructor

        public ProductsViewModel()
        {

            //Field section
            _dialogCoordinator = DialogCoordinator.Instance;
            _unmodifiedProducts = ProductsService.GetProducts();
            _Products = new ObservableCollection<Product>(_unmodifiedProducts);


            //Command section
            SearchCommand = new RelayCommand(Search);

        }

        #endregion

        #region Properties and fields

        /// <summary>
        /// The mode through i can play with dialogs from the view model.
        /// </summary>
        private IDialogCoordinator _dialogCoordinator;



        /// <summary>
        /// The list of Products.
        /// </summary>
        private List<Product> _unmodifiedProducts;
        private ObservableCollection<Product> _Products;
        public ObservableCollection<Product> Products
        {
            get => _Products;
            set => Set(ref _Products, value);
        }


        #endregion

        #region Properties and fields specific for the options page

        /// <summary>
        /// The remove button will be available only if
        /// there is at least one element selected.
        /// </summary>
        private bool _isRemoveButtonEnabled = false;
        public bool IsRemoveButtonEnabled
        {
            get => _isRemoveButtonEnabled;
            set => Set(ref _isRemoveButtonEnabled, value);
        }

        /// <summary>
        /// The string to search for in the list of Productss.
        /// </summary>
        private string _Productsearch;
        public string Productsearch
        {
            get => _Productsearch;
            set => Set(ref _Productsearch, value);
        }

        #endregion

        #region Commands

        /// <summary>
		/// The command that updates the datagrid accordingly to the user input's search.
		/// </summary>
		public RelayCommand SearchCommand { get; set; }
        private void Search()
        {
            if (Productsearch == string.Empty || Productsearch==null   )
                Products = new ObservableCollection<Product>(_unmodifiedProducts);
            else
            {
                Products = new ObservableCollection<Product>(_unmodifiedProducts.Where(x => x.ProductName.Contains(Productsearch)));
            }

        }


        #endregion
    }

}