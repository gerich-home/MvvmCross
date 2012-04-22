using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using System.Collections;
using Cirrious.MvvmCross.Interfaces.Commands;
using Cirrious.MvvmCross.Binding.Android;

namespace Cirrious.MvvmCross.Binding
{
	
	
	public class MvxBindableGridView
        : GridView
	{
		public MvxBindableGridView (Context context, IAttributeSet attrs)
            : this(context, attrs, new Cirrious.MvvmCross.Binding.Android.Views.MvxBindableListAdapter(context))
		{
		}

		public MvxBindableGridView (Context context, IAttributeSet attrs, Cirrious.MvvmCross.Binding.Android.Views.MvxBindableListAdapter adapter)
            : base(context, attrs)
		{
			var itemTemplateId = Cirrious.MvvmCross.Binding.Android.Views.MvxBindableListViewHelpers.ReadTemplatePath (context, attrs);
			adapter.ItemTemplateId = itemTemplateId;
			Adapter = adapter;
			SetupItemClickListener ();
		}

		public new Cirrious.MvvmCross.Binding.Android.Views.MvxBindableListAdapter Adapter
		{
			get { return base.Adapter as Cirrious.MvvmCross.Binding.Android.Views.MvxBindableListAdapter; }
			set
			{
				var existing = Adapter;
				if (existing == value)
				{
					return;
				}

				if (existing != null && value != null)
				{
					value.ItemsSource = existing.ItemsSource;
					value.ItemTemplateId = existing.ItemTemplateId;
				}

				base.Adapter = value;
			}
		}

		public IList ItemsSource
		{
			get { return Adapter.ItemsSource; }
			set { Adapter.ItemsSource = value; }
		}

		public int ItemTemplateId
		{
			get { return Adapter.ItemTemplateId; }
			set { Adapter.ItemTemplateId = value; }
		}

		public new IMvxCommand ItemClick { get; set; }

		private void SetupItemClickListener ()
		{
			base.ItemClick += (sender, args) =>
			{
				if (this.ItemClick == null)
				{
					return;
				}
				var item = Adapter.GetItem (args.Position) as MvxJavaContainer;
				if (item == null)
				{
					return;
				}

				if (item.Object == null)
				{
					return;
				}

				if (!this.ItemClick.CanExecute (item.Object))
				{
					return;
				}

				this.ItemClick.Execute (item.Object);
			};
		}
	}
}

