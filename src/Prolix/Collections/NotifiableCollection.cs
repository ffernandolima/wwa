// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

using Prolix.Extensions.Collections;

namespace Prolix.Collections
{
    /// <summary>
    /// Represents a dynamic data collection that provides notifications 
    /// when items get added, removed, refreshed, or any item is changed
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection</typeparam>
    public class NotifiableCollection<T> : ObservableCollection<T>
		where T : class, INotifyPropertyChanged
	{
		#region Events

		/// <summary>
		/// Is fired when a items changed
		/// </summary>
		public event NotifyItemChangedEventHandler<T> ItemChanged;

		#endregion

		#region Constructors

		public NotifiableCollection()
		{
			NotifyItem = true;
		}

		public NotifiableCollection(bool notifyItem)
		{
			NotifyItem = notifyItem;
		}

		public NotifiableCollection(IEnumerable<T> source) : base(source)
		{
			NotifyItem = true;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Calls CollectionChanged event if the a item is changed
		/// </summary>
		public bool NotifyItem { get; set; }

        #endregion

        #region Public Methods

        /// <summary> 
        /// Adds the elements of the specified collection to the end of the ObservableCollection(Of T). 
        /// </summary> 
        public void AddRange(IEnumerable<T> collection, NotifyCollectionChangedAction notificationMode = NotifyCollectionChangedAction.Add)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            CheckReentrancy();

            if (notificationMode == NotifyCollectionChangedAction.Reset)
            {
                foreach (var i in collection)
                {
                    Items.Add(i);
                }

                OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

                return;
            }

            int startIndex = Count;
            var changedItems = collection as List<T> ?? new List<T>(collection);
            foreach (var i in changedItems)
            {
                Items.Add(i);
            }

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, changedItems, startIndex));
        }

        /// <summary> 
        /// Removes the first occurence of each item in the specified collection from ObservableCollection(Of T). 
        /// </summary> 
        public void RemoveRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            foreach (var i in collection)
                Items.Remove(i);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary> 
        /// Clears the current collection and replaces it with the specified item. 
        /// </summary> 
        public void Replace(T item)
        {
            ReplaceRange(new[] { item });
        }

        /// <summary> 
        /// Clears the current collection and replaces it with the specified collection. 
        /// </summary> 
        public void ReplaceRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            Items.Clear();
            AddRange(collection, NotifyCollectionChangedAction.Reset);
        }

        #endregion

        #region Events Handlers

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			var newItems = e.NewItems.ToList<INotifyPropertyChanged>();
			var oldItems = e.OldItems.ToList<INotifyPropertyChanged>();

			foreach (var item in newItems)
			{
				item.PropertyChanged += Item_PropertyChanged;
			}

			foreach (var item in oldItems)
			{
				item.PropertyChanged -= Item_PropertyChanged;
			}

			base.OnCollectionChanged(e);
		}

        protected override void ClearItems()
        {
            foreach (var item in this)
            {
                item.PropertyChanged -= Item_PropertyChanged;
            }

            base.ClearItems();
        }

        #endregion

        #region Events Handlers

        void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var item = sender as T;
			var args = new NotifyItemChangedEventArgs<T>(item, e.PropertyName);

            ItemChanged?.Invoke(this, args);

			if (NotifyItem)
			{
				var index = IndexOf(item);
				this[index] = item;
			}
		}

		#endregion
	}
}
