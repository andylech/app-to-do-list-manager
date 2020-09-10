# To-Do List Manager
A sample to-do list manager app in Xamarin.Forms for a potential job. It is over-architected for the requirements on purpose to demonstrate a performant Xamarin.Forms MVVM architecture I have used on past projects.

---

### Features

* Model-View-ViewModel architecture
* ViewModel navigation
* Common navigation router
* Page type for navigation decisions
* **TODO**

---

### Issue Tracking


The status of the project can be viewed here:
https://app.gitkraken.com/glo/board/X1lZaz2bBQARvJyD

---

### Requirements
* **TODO**

---

### Dependencies

* FancyLogger: My own NuGet for formatting output, particularly to DEBUG, so it won't be lost in the clutter of Android logs in particular.  Also, works well with [VSColorOutput](https://mike-ward.net/vscoloroutput/).

---

### Caveats

* I could easily use an IoC container to handle constructor injection, replace the Services.ServiceManager and make other improvements, but I was trying not to overdue the dependencies of this simple demo project.
* The messaging service used here is a simple static one for displaying exceptions.  A full-blown app could, of course, use Xamarin.Forms MessagingCenter for things like analytics reporting, displaying alerts when not convenient (PageModels), etc.
