using System;
using System.Collections.Generic;
using Foundation;
using Mobile.Core.Models;
using Mobile.iOS.Views.Cells;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace Mobile.iOS.TableSources
{
    public class UsersTableSource : MvxTableViewSource
    {
		static readonly NSString UserCellIdentifier = new NSString("UserCell");
        List<User> _users;


		public UsersTableSource(List<User> users, UITableView tableView) : base(tableView)
        {
            _users = users;
            tableView.RegisterClassForCellReuse(typeof(UserCell), "UserCell");
        }

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			if (_users == null)
			{
				return 0;
			}
			return _users.Count;
		}

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return (UserCell)tableView.DequeueReusableCell("UserCell");
        }
    }
}
