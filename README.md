**[View document in Syncfusion Xamarin Knowledge base](https://www.syncfusion.com/kb/12332/how-to-customize-the-load-more-in-xamarin-forms-listview-sflistview)**

## Sample

```xaml
 <local:ExtendedListView x:Name="listView" IsStickyFooter="True" ItemsSource="{Binding Products}" AutoFitMode="Height">
    <local:ExtendedListView.ItemTemplate>
        <DataTemplate>
            <code>
            . . .
            . . .
            <code>
        </DataTemplate>
    </local:ExtendedListView.ItemTemplate>
    <local:ExtendedListView.FooterTemplate>
        <DataTemplate>
            <Grid VerticalOptions="FillAndExpand" HorizontalOptions="FillAndExpand" HeightRequest="50">
                    <Grid.GestureRecognizers>
                        <TapGestureRecognizer Command="{Binding LoadMoreItemsCommand}"/>
                    </Grid.GestureRecognizers>
                    <Label Text="Tap here to load more" FontSize="15" FontAttributes="Bold" />
                    <busyindicator:SfBusyIndicator AnimationType="Cupertino" IsBusy="{Binding Path=BindingContext.IsBusy, Source={x:Reference listView}}" IsVisible="{Binding Path=BindingContext.IsBusy, Source={x:Reference listView}}" BackgroundColor="White"/>
                </Grid>
        </DataTemplate>
    </local:ExtendedListView.FooterTemplate>
</local:ExtendedListView>

C#:

container.PropertyChanged += Container_PropertyChanged;

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

ViewModel.cs:

public Command LoadMoreItemsCommand { get; set; }

LoadMoreItemsCommand = new Command(LoadMoreItems, CanLoadMoreItems);

private bool CanLoadMoreItems()
{
    if (Products.Count >= totalItems)
        return false;
    return true;
}

private async void LoadMoreItems()
{
    try
    {
        IsBusy = true;
        await Task.Delay(2000);
        var index = Products.Count;
        var count = index + 3 >= totalItems ? totalItems - index : 3;
        AddProducts(index, count);
    }
    catch
    {

    }
    finally
    {
        IsBusy = false;
    }
}

private void AddProducts(int index, int count)
{
    for (int i = index; i < index + count; i++)
    {
        var name = Names[i];
        var p = new Product()
        {
            Name = name,
            Weight = Weights[i],
            Price = Prices[i],
            Image = ImageSource.FromResource("ListViewXamarin.LoadMore." + name.Replace(" ", string.Empty) + ".jpg")
        };

        Products.Add(p);
    }
}
```
