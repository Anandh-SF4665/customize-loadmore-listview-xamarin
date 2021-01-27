using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SfListViewSample
{
    #region ExtendedListView
    public class ExtendedListView : SfListView
    {
        VisualContainer container;
        public ExtendedListView()
        {
            container = this.GetVisualContainer();
            container.PropertyChanged += Container_PropertyChanged;
        }

        private void Container_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var extent = (double)container.GetType().GetRuntimeProperties().FirstOrDefault(container => container.Name == "TotalExtent").GetValue(container);
                if (e.PropertyName == "Height")
                {
                    if (extent > container.ScrollRows.ViewSize && this.IsStickyFooter)
                        this.IsStickyFooter = false;
                    else if (extent <= container.ScrollRows.ViewSize && !this.IsStickyFooter)
                        this.IsStickyFooter = true;
                }
            });
        }
    }
    #endregion
}
