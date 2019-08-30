using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

/*
 * Use of online code
 * Martin Schreiber
 * https://schreibermartin.wordpress.com/
 * WinForms TreeView Data Binding – Part 1 of 2
 * WinForms TreeView Data Binding – Part 2 of 2
 * https://schreibermartin.wordpress.com/2014/12/16/winforms-treeview-data-binding-part-1-of-2/
 * https://schreibermartin.wordpress.com/2014/12/16/winforms-treeview-data-binding-part-2-of-2/
 * Dec 16, 2014
*/
namespace TreeViewDataBindings
{
	public class TreeViewBinding<TDataItem> where TDataItem : class
	{
		private TreeView _treeView;
		private TreeNodeCollection _treeNodeCollection;
		private BindingSource _bindingSource;
		private Func<object, TDataItem> _getDataItemFunc;
		private Func<TDataItem, TreeNode> _addTreeNodeFunc;
		private Action<TDataItem, TreeNode> _updateTreeNodeAction;
		private TreeNode _currentAddItem;
		private TreeNode _parentTreeNode;

		public TreeViewBinding(
			TreeView treeView,
			BindingSource bindingSource,
			Func<object, TDataItem> getDataItemFunc,
			Func<TDataItem, TreeNode> addTreeNodeFunc,
			Action<TDataItem, TreeNode> updateTreeNodeAction
			) : this(treeView, treeView.Nodes, null, bindingSource,
						getDataItemFunc, addTreeNodeFunc, updateTreeNodeAction
			)
		{ }

		public TreeViewBinding(
			TreeNode parentTreeNode,
			BindingSource bindingSource,
			Func<object, TDataItem> getDataItemFunc,
			Func<TDataItem, TreeNode> addTreeNodeFunc,
			Action<TDataItem, TreeNode> updateTreeNodeAction
			) : this(parentTreeNode.TreeView, parentTreeNode.Nodes,
						parentTreeNode, bindingSource,
						getDataItemFunc, addTreeNodeFunc, updateTreeNodeAction
			)
		{ }

		private TreeViewBinding(
			TreeView treeView,
			TreeNodeCollection treeNodeCollection,
			TreeNode parentTreeNode,
			BindingSource bindingSource,
			Func<object, TDataItem> getDataItemFunc,
			Func<TDataItem, TreeNode> addTreeNodeFunc,
			Action<TDataItem, TreeNode> updateTreeNodeAction
			)
		{
			if (treeView == null)
				throw new ArgumentNullException("treeView");
			if (treeNodeCollection == null)
				throw new ArgumentNullException("treeNodeCollection");
			if (bindingSource == null)
				throw new ArgumentNullException("bindingSource");
			if (bindingSource == null)
				throw new ArgumentNullException("bindingSource");
			if (getDataItemFunc == null)
				throw new ArgumentNullException("getDataItemFunc");
			if (addTreeNodeFunc == null)
				throw new ArgumentNullException("addTreeNodeFunc");
			if (updateTreeNodeAction == null)
				throw new ArgumentNullException("updateTreeNodeAction");

			_treeView = treeView;
			_treeNodeCollection = treeNodeCollection;
			_parentTreeNode = parentTreeNode; // may be null.
			_bindingSource = bindingSource;
			_bindingSource.AllowNew = true;
			_getDataItemFunc = getDataItemFunc;
			_addTreeNodeFunc = addTreeNodeFunc;
			_updateTreeNodeAction = updateTreeNodeAction;
			// sync to binding source's current items and selection.
			AddExistingItems();
			_bindingSource.ListChanged += (s, e) =>
			{
				switch (e.ListChangedType)
				{
					case ListChangedType.ItemAdded:
						AddItem(e.NewIndex);
						SelectItem();
						break;
					case ListChangedType.ItemChanged:
						UpdateItem();
						break;
					case ListChangedType.ItemDeleted:
						DeleteItem(e.NewIndex);
						break;
					case ListChangedType.ItemMoved:
						MoveItem(e.OldIndex, e.NewIndex);
						break;
					case ListChangedType.PropertyDescriptorAdded:
					case ListChangedType.PropertyDescriptorChanged:
					case ListChangedType.PropertyDescriptorDeleted:
						break;
					case ListChangedType.Reset:
						AddExistingItems();
						SelectItem();
						break;
					default:
						throw new NotImplementedException("...");
				}
			};
			_bindingSource.PositionChanged += (s, e) => SelectItem();
			_treeView.AfterSelect += AfterNodeSelect;
			SelectItem();
		}

		private void AfterNodeSelect(object sender, TreeViewEventArgs e)
		{
			var treeNode = e.Node;
			// Skip, if the TreeNode belongs to a foreign collection.
			if (treeNode.Parent != _parentTreeNode) return;
			_bindingSource.Position = treeNode.Index;
		}

		private void AddExistingItems()
		{
			foreach (var listItem in _bindingSource.List)
			{
				TDataItem dataItem = _getDataItemFunc(listItem);
				TreeNode treeNode = _addTreeNodeFunc(dataItem);
				if (treeNode == null) continue;
				_updateTreeNodeAction(dataItem, treeNode);
				_treeNodeCollection.Add(treeNode);
			}
		}

		private void AddItem(int newIndex)
		{
			TDataItem dataItem = _getDataItemFunc(_bindingSource.Current);
			if (_currentAddItem == null)
			{
				TreeNode treeNode = _addTreeNodeFunc(dataItem);
				if (treeNode == null) return;
				_treeNodeCollection.Insert(newIndex, treeNode);
				_currentAddItem = treeNode;
				return;
			}
			_updateTreeNodeAction(dataItem, _currentAddItem);
			_currentAddItem = null;
		}

		public void UpdateItem()
		{
			if (_bindingSource.Current == null) return;
			TDataItem dataItem = _getDataItemFunc(_bindingSource.Current);
			var treeNode = _treeNodeCollection[_bindingSource.Position];
			_updateTreeNodeAction(dataItem, treeNode);
		}

		private void DeleteItem(int index)
		{
			_treeNodeCollection.RemoveAt(index);
			_currentAddItem = null;
		}

		private void MoveItem(int oldIndex, int newIndex)
		{
			var treeNode = _treeNodeCollection[_bindingSource.Position];
			_treeNodeCollection.RemoveAt(oldIndex);
			_treeNodeCollection.Insert(newIndex, treeNode);
		}

		private void SelectItem()
		{
			if (_bindingSource.Position < 0) return;
			if (_treeNodeCollection.Count <= _bindingSource.Position) return;
			var treeNode = _treeNodeCollection[_bindingSource.Position];
			treeNode.EnsureVisible();
			_treeView.SelectedNode = treeNode;
		}

	}
}
