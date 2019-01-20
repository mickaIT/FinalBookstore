﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryLogic.Model
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="bookstore")]
	public partial class BookstoreDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertBook(Book instance);
    partial void UpdateBook(Book instance);
    partial void DeleteBook(Book instance);
    partial void InsertLibraryState(LibraryState instance);
    partial void UpdateLibraryState(LibraryState instance);
    partial void DeleteLibraryState(LibraryState instance);
    partial void InsertSale(Sale instance);
    partial void UpdateSale(Sale instance);
    partial void DeleteSale(Sale instance);
    #endregion
		
		public BookstoreDataContext() : 
				base(global::LibraryLogic.Properties.Settings.Default.bookstoreConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public BookstoreDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BookstoreDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BookstoreDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BookstoreDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Book> Books
		{
			get
			{
				return this.GetTable<Book>();
			}
		}
		
		public System.Data.Linq.Table<LibraryState> LibraryStates
		{
			get
			{
				return this.GetTable<LibraryState>();
			}
		}
		
		public System.Data.Linq.Table<Sale> Sales
		{
			get
			{
				return this.GetTable<Sale>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Book")]
	public partial class Book : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _ISBN;
		
		private string _Title;
		
		private string _Author;
		
		private string _Genre;
		
		private int _Count;
		
		private bool _BookState;
		
		private EntitySet<LibraryState> _LibraryStates;
		
		private EntitySet<Sale> _Sales;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnISBNChanging(int value);
    partial void OnISBNChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnAuthorChanging(string value);
    partial void OnAuthorChanged();
    partial void OnGenreChanging(string value);
    partial void OnGenreChanged();
    partial void OnCountChanging(int value);
    partial void OnCountChanged();
    partial void OnBookStateChanging(bool value);
    partial void OnBookStateChanged();
    #endregion
		
		public Book()
		{
			this._LibraryStates = new EntitySet<LibraryState>(new Action<LibraryState>(this.attach_LibraryStates), new Action<LibraryState>(this.detach_LibraryStates));
			this._Sales = new EntitySet<Sale>(new Action<Sale>(this.attach_Sales), new Action<Sale>(this.detach_Sales));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ISBN", DbType="Int")]
		public int ISBN
		{
			get
			{
				return this._ISBN;
			}
			set
			{
				if ((this._ISBN != value))
				{
					this.OnISBNChanging(value);
					this.SendPropertyChanging();
					this._ISBN = value;
					this.SendPropertyChanged("ISBN");
					this.OnISBNChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="VarChar(50)", CanBeNull=false)]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this.OnTitleChanging(value);
					this.SendPropertyChanging();
					this._Title = value;
					this.SendPropertyChanged("Title");
					this.OnTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Author", DbType="VarChar(50)", CanBeNull=false)]
		public string Author
		{
			get
			{
				return this._Author;
			}
			set
			{
				if ((this._Author != value))
				{
					this.OnAuthorChanging(value);
					this.SendPropertyChanging();
					this._Author = value;
					this.SendPropertyChanged("Author");
					this.OnAuthorChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Genre", DbType="VarChar(50)", CanBeNull=false)]
		public string Genre
		{
			get
			{
				return this._Genre;
			}
			set
			{
				if ((this._Genre != value))
				{
					this.OnGenreChanging(value);
					this.SendPropertyChanging();
					this._Genre = value;
					this.SendPropertyChanged("Genre");
					this.OnGenreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Count", DbType="Int")]
		public int Count
		{
			get
			{
				return this._Count;
			}
			set
			{
				if ((this._Count != value))
				{
					this.OnCountChanging(value);
					this.SendPropertyChanging();
					this._Count = value;
					this.SendPropertyChanged("Count");
					this.OnCountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BookState", DbType="Bit")]
		public bool BookState
		{
			get
			{
				return this._BookState;
			}
			set
			{
				if ((this._BookState != value))
				{
					this.OnBookStateChanging(value);
					this.SendPropertyChanging();
					this._BookState = value;
					this.SendPropertyChanged("BookState");
					this.OnBookStateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Book_LibraryState", Storage="_LibraryStates", ThisKey="Id", OtherKey="BookStoreBooks")]
		public EntitySet<LibraryState> LibraryStates
		{
			get
			{
				return this._LibraryStates;
			}
			set
			{
				this._LibraryStates.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Book_Sale", Storage="_Sales", ThisKey="Id", OtherKey="SoldBook")]
		public EntitySet<Sale> Sales
		{
			get
			{
				return this._Sales;
			}
			set
			{
				this._Sales.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_LibraryStates(LibraryState entity)
		{
			this.SendPropertyChanging();
			entity.Book = this;
		}
		
		private void detach_LibraryStates(LibraryState entity)
		{
			this.SendPropertyChanging();
			entity.Book = null;
		}
		
		private void attach_Sales(Sale entity)
		{
			this.SendPropertyChanging();
			entity.Book = this;
		}
		
		private void detach_Sales(Sale entity)
		{
			this.SendPropertyChanging();
			entity.Book = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.LibraryState")]
	public partial class LibraryState : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _BookStoreBooks;
		
		private int _BookInvoices;
		
		private EntityRef<Book> _Book;
		
		private EntityRef<Sale> _Sale;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnBookStoreBooksChanging(int value);
    partial void OnBookStoreBooksChanged();
    partial void OnBookInvoicesChanging(int value);
    partial void OnBookInvoicesChanged();
    #endregion
		
		public LibraryState()
		{
			this._Book = default(EntityRef<Book>);
			this._Sale = default(EntityRef<Sale>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BookStoreBooks", DbType="Int")]
		public int BookStoreBooks
		{
			get
			{
				return this._BookStoreBooks;
			}
			set
			{
				if ((this._BookStoreBooks != value))
				{
					if (this._Book.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnBookStoreBooksChanging(value);
					this.SendPropertyChanging();
					this._BookStoreBooks = value;
					this.SendPropertyChanged("BookStoreBooks");
					this.OnBookStoreBooksChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BookInvoices", DbType="Int")]
		public int BookInvoices
		{
			get
			{
				return this._BookInvoices;
			}
			set
			{
				if ((this._BookInvoices != value))
				{
					if (this._Sale.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnBookInvoicesChanging(value);
					this.SendPropertyChanging();
					this._BookInvoices = value;
					this.SendPropertyChanged("BookInvoices");
					this.OnBookInvoicesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Book_LibraryState", Storage="_Book", ThisKey="BookStoreBooks", OtherKey="Id", IsForeignKey=true)]
		public Book Book
		{
			get
			{
				return this._Book.Entity;
			}
			set
			{
				Book previousValue = this._Book.Entity;
				if (((previousValue != value) 
							|| (this._Book.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Book.Entity = null;
						previousValue.LibraryStates.Remove(this);
					}
					this._Book.Entity = value;
					if ((value != null))
					{
						value.LibraryStates.Add(this);
						this._BookStoreBooks = value.Id;
					}
					else
					{
						this._BookStoreBooks = default(int);
					}
					this.SendPropertyChanged("Book");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Sale_LibraryState", Storage="_Sale", ThisKey="BookInvoices", OtherKey="Id", IsForeignKey=true)]
		public Sale Sale
		{
			get
			{
				return this._Sale.Entity;
			}
			set
			{
				Sale previousValue = this._Sale.Entity;
				if (((previousValue != value) 
							|| (this._Sale.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Sale.Entity = null;
						previousValue.LibraryStates.Remove(this);
					}
					this._Sale.Entity = value;
					if ((value != null))
					{
						value.LibraryStates.Add(this);
						this._BookInvoices = value.Id;
					}
					else
					{
						this._BookInvoices = default(int);
					}
					this.SendPropertyChanged("Sale");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Sale")]
	public partial class Sale : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private System.Nullable<int> _SoldBook;
		
		private string _InvoiceDate;
		
		private EntitySet<LibraryState> _LibraryStates;
		
		private EntityRef<Book> _Book;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnSoldBookChanging(System.Nullable<int> value);
    partial void OnSoldBookChanged();
    partial void OnInvoiceDateChanging(string value);
    partial void OnInvoiceDateChanged();
    #endregion
		
		public Sale()
		{
			this._LibraryStates = new EntitySet<LibraryState>(new Action<LibraryState>(this.attach_LibraryStates), new Action<LibraryState>(this.detach_LibraryStates));
			this._Book = default(EntityRef<Book>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SoldBook", DbType="Int")]
		public System.Nullable<int> SoldBook
		{
			get
			{
				return this._SoldBook;
			}
			set
			{
				if ((this._SoldBook != value))
				{
					if (this._Book.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnSoldBookChanging(value);
					this.SendPropertyChanging();
					this._SoldBook = value;
					this.SendPropertyChanged("SoldBook");
					this.OnSoldBookChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_InvoiceDate", DbType="VarChar(50)")]
		public string InvoiceDate
		{
			get
			{
				return this._InvoiceDate;
			}
			set
			{
				if ((this._InvoiceDate != value))
				{
					this.OnInvoiceDateChanging(value);
					this.SendPropertyChanging();
					this._InvoiceDate = value;
					this.SendPropertyChanged("InvoiceDate");
					this.OnInvoiceDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Sale_LibraryState", Storage="_LibraryStates", ThisKey="Id", OtherKey="BookInvoices")]
		public EntitySet<LibraryState> LibraryStates
		{
			get
			{
				return this._LibraryStates;
			}
			set
			{
				this._LibraryStates.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Book_Sale", Storage="_Book", ThisKey="SoldBook", OtherKey="Id", IsForeignKey=true)]
		public Book Book
		{
			get
			{
				return this._Book.Entity;
			}
			set
			{
				Book previousValue = this._Book.Entity;
				if (((previousValue != value) 
							|| (this._Book.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Book.Entity = null;
						previousValue.Sales.Remove(this);
					}
					this._Book.Entity = value;
					if ((value != null))
					{
						value.Sales.Add(this);
						this._SoldBook = value.Id;
					}
					else
					{
						this._SoldBook = default(Nullable<int>);
					}
					this.SendPropertyChanged("Book");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_LibraryStates(LibraryState entity)
		{
			this.SendPropertyChanging();
			entity.Sale = this;
		}
		
		private void detach_LibraryStates(LibraryState entity)
		{
			this.SendPropertyChanging();
			entity.Sale = null;
		}
	}
}
#pragma warning restore 1591