# Movie Booking App

This file provides a high level explanation of concepts used in Movie Booking App. Before diving into the working of the app, I would like to call out that the intention of Movie Booking App is to make the reader familiar with Entity Framework Core and Razor pages. We have already seen and worked with Entity Framework core in the sample application, where we did CRUD operations using Entity Framework Core.
We touched upon Razor pages in earlier chapter of the book, but did not see it in detail, so we will discuss Razor page. Post this the code of movie booking app would be easily understandable.

## Razor Pages

Recall that even if we need to create a simple view using MVC framework, we need to create a model, view and controller. At times model may not be needed (by using ViewBag), but even in this scenario, we still need to create a controller and a view. This is bare minimum work that we need to do, to create the view and wire it up.
ASP.NET Core 2.0 comes up with a new framework for creating the pages, without the complexity and work that comes by virtue of using MVC. Its pretty safe to say, that Razor pages are leaner version of MVC and reminds us of the old school aspx web forms.
Razor Pages makes the UI development very productive and easier. And the best part is that you need not do anything extra to leverage the goodness of Razor pages. The normal ASP.NET Core 2.0 web project is ready to use this new feature of ASP.NET Core 2.0.
If we were to create a view named Index, we would create a HomeController (or any other controller as required), then add an action named Index and finally, return View in the action, which would wire up to Index.cshtml Razor view. This Index.cshtml view sits inside Views folder in the subfolder named Home and looks like:
```
@{
    ViewData["Title"] = "Home Page";
}

<!--HTML/Razor markup for the view -->
```

and in the solution explorer, the location is as shown below:

![Razor View](https://github.com/packtpavanr/.NET-Core-2.0-By-Example/blob/master/MovieBooking/wwwroot/images/RazorView.png)

Now, lets see how we can achieve the same using the new Razor pages. Add new item to your project, below dialog would display

![Add RazorPage](https://github.com/packtpavanr/.NET-Core-2.0-By-Example/blob/master/MovieBooking/wwwroot/images/RazorPage.png)

Choose RazorPage as shown above and name it as Index.cshtml

Below is how a simple Razor page corresponding to Index action (that we defined above) looks like:

```
@page
@model IndexModel
@{
    ViewData["Title"] = "Home page";
}
```

Notice that RazorPage looks very similar to the view we created, just that it has two additional lines of code in  
`@page` directive. This directive tells the Razor engine that Hey! I am a cshtml file, but I am a Razor page (and not view :)). Treat me as an action! 
`@model IndexModel` Indicates the model to be used for page. But this is same as MVC Razor view. If Index.cshtml view also had a model, it would also have looked the same.  

So now, we know that Razor pages are very similar to the Razor view, just that they have `@page` as the first Razor directive. `@page` must be the first Razor directive as it affects the behavior of other Razor constructs.
Rest of the markup can be same as we do in Razor view.

Index.cshtml Razor page that we created went inside the Pages folder. This is the default location for the Razor pages, but can be changed by the below code in the `Startup.cs`.  
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc()
        .AddRazorPagesOptions(options =>
        {
            options.RootDirectory = "/RishabhPages";            
        });
}
``` 
With this, we can place the Razor pages in the directory named RishabhPages in the project.  
You would also notice that we do not have any Views and Controllers folder. So what happens to the Layout, ViewImports, ViewStart views that we saw earlier. They are now created right inside Pages folder and there is no Shared folder that we see in Views. 

We can use and do everything inside Razor pages that comes with MVC, so model binding works as is. We have handlers in razor page, which can do the same work as action methods do in controller. All the goodness of Tag Helpers and Html helpers is available in Razor pages as well. The routing is also pretty simple. The path of the Razor page in the project file system determines the matching url. To give an example, below would be the mapping for file system path and url

Physical File System Path | **URL**
--- | --- 
~Pages/Index.cshtml | **/** or **/Index**
~Pages/Orders/Index.cshtml | **/Orders** or **/Orders/Index**
~Pages/Orders/Edit.cshtml | **/Orders/Edit/1**
~Pages/Orders/Create.cshtml | **/Orders/Create**

Route can be configured in the `@page` directive. For example, if your Razor page expects an order id (lets name the property as id) for editing the order, we can define the page route as below:
```
@page "{id}"
```  
or  
```
@page "{id:int}"
```  
If you wish to pass multiple parameters like this, we can do so for as many parameters as we want. The below code sample demonstrates two parameters
```
@page "{id:int}/{seats:int}"
```  
This much information on routing would be good enough to complete our sample app. Detailed coverage on routing is beyond the scope of this by example book, so interested readers may want to read more about routing [here](https://docs.microsoft.com/en-us/aspnet/core/mvc/razor-pages/razor-pages-convention-features?view=aspnetcore-2.0)

Next lets look at the `IndexModel`  
![Index Model](https://github.com/packtpavanr/.NET-Core-2.0-By-Example/blob/master/MovieBooking/wwwroot/images/IndexModel.png)  
We see that the Index.cshtml also has a code behind file named Index.cshtml.cs (and that's why it is similar to aspx pages, where we used to have .aspx.cs/.aspx.vb as code behind files). This provides neat code seperation, i.e. Razor inside cshtml and the C# code in code behind file. This code behind file is also the model of the page.  
Let's have a look at the code behind **.cs** file
```
namespace MovieBooking.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }        
    }
}
```  
`IndexModel` itself derives from `PageModel` and gets all the properties that we see in the image above. Notice that the convention of model class is `<PageName>Model`. For Index page, it becomes `IndexModel` and so on. We can define properties, handlers (think of them as actions) and other logic in this file and this is what makes the Razor Pages extremely powerful and productive. We can do dependency injection in the code behind pages, the same way we did for controllers

Notice that the `IndexModel` (created from default template of PageModel) has a method named `OnGet()`. This is the handler for the GET HTTP Request. Like wise we can add handlers for any HTTP verb, like PUT, POST, DELETE. We can also have `async` handlers as shown below
```
public async Task<IActionResult> OnGetAsync(int id)
{
}

public async Task<IActionResult> OnPostAsync()
{
     if (!ModelState.IsValid)
     {
          return Page();
     }
     
     return this.RedirectToPage("/BookTicket", this.SelectedMovieId);
}
```  
Notice an important difference between `OnGet` and `OnGetAsync` method. Though one is `async` version, notice that synchronous method has `void` return type, while `async` version has `Task<IActionResult>` and not `void`. This is important. Failure to have `Task` as return type would result in razor page getting rendered even before `OnGet` method being executed.`OnGet` handler is used to initialize the state of the page, when its loaded. So, for example, suppose we have have an Order, that we want to Create and later edit, then we can have two Razor pages, one for create and one for edit. Each page would have at-least 2 handlers, one to load the page and another one to submit the data and navigate to next page. Let's have a look
Create Razor Page:  
`OnGet` handler of the page would be simple, it would load an empty form, so not much to do here. The user would then fill up the form with required details and submit it. This would call the `OnPost` handler, where we would want the data submitted by the user in the form. To get the submitted data, we will do the following:
1. Define a property in the PageModel, with a `[BindProperty]` attribute. This attribute is used to opt in to Model binding
```
[BindProperty]
public Order Order {get;set;}

```  
By default, only non-GET verbs bind properties in Razor pages.  
2. On the cshtml side, we need to ensure that we actually bind to this property like
```
<form method="post">
    <input type="text" asp-for="@Model.Order.Name"/>

    <!-- Other form controls, not shown for brevity and simplicity -->
    <input type="submit" value="Submit"/>
</form>
```  
3. In the `OnPost` handler, we can now read and process the submitted data as shown below:
```
public IActionResult OnPost()
{
    if(!this.ModelState.IsValid)
    {
        return this.Page();
    }
    
    var submittedData = this.Order;
    //// Do processing, manipulation or persist in DB as needed.
}
```  
Edit Razor page:  
1. For the edit screen, we would need to load the order details in `OnGet` handler of the page. To load the order, we would need `Id` property, so while invoking this page, `Id` would need to be passed. This can be achieved in the cshtml as shown below:
```
<a asp-page="Edit" asp-route-id=@item.Id >Edit Order</a>
```  
Notice, how easy it is to wire up a page and parameter it needs, by using the tag helpers. `asp-page` tag helper, specifies the page that needs to be called and the route data (id in our case) can be passed by using the `asp-route-{route parameter}` tag helper.

2. Next, our razor page must accept the `id` as the route parameter and this can be easily achieved in our `Edit.cshtml` with the code `@page "{id:int}"`  
3. Finally in our `OnGet` handler, we can have a parameter as `id` as shown below:
```
public void OnGet(int id)
{
    //// Load order by using the id , parameter 
}
```  
Another important aspect to note here is there is only one default handler for each of the HTTP verb. What if we need multiple HTTP GET or POST handlers? We can create our own handlers. The convention to name is `OnGet{handlerName}` or `OnPost{handlerName}`. The handler can be invoked in multiple ways as shown below:  
1. Query string
```
/edit/1/?handler=handlerName
``` 
2. Define a route and pass the handler in url
```
@page "{handler?}"  
<!-- use is as /edit/1/handlerName -->
```  
3. Tag helper  
```
<input type=”submit” asp-page-handler="handlerName" value="value" />
```  
This discussion should be enough to get us started on Razor pages and use it effectively. I hope this would make the UI development pretty fast and productive. So do we get rid of MVC? No, use Razor pages for web UI development and MVC for Web API development.

For a thorough and detailed understanding on Razor page, please read [this](https://docs.microsoft.com/en-us/aspnet/core/mvc/razor-pages/?view=aspnetcore-2.0&tabs=visual-studio)

Happy Coding!














