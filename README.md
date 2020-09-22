# To-Do List Manager
A sample to-do list manager in Xamarin.Forms showing off ViewModel navigation and other features in Reactive UI

---

## [ReactiveUI Version - In Progress](https://github.com/andylech/app-to-do-list-manager/tree/develop)

### Core Features

* Model-View-ViewModel architecture
* PageModel (ViewModel) navigation
* Common navigation router
* Page (View) type for navigation logic
* Mock API layer for development and testing
* **TODO**

### Future Features

* [SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr) API for multi-device synchronization

### Issue Tracking

The status of the project can be viewed here:
https://app.gitkraken.com/glo/board/X2f4NyQxbQAR8Ttx


### Requirements
* **TODO**

### Dependencies

<!-- * [FancyLogger](https://github.com/xamarinfiles/library-fancy-logger): My own NuGet for formatting output, particularly to DEBUG, so it won't be lost in the clutter of Android logs in particular.  Also, works well with [VSColorOutput](https://mike-ward.net/vscoloroutput/). -->
* **TODO**



---

## [Original Version - Halted](https://github.com/andylech/app-to-do-list-manager/tree/akavache)

I did this originally as an coding assignment for a potential job.  The assignment was to create a real-world example of a to-do list app for multiple lists but without specifying any particular data architecture.  I generally work data-first, bottom-up and creating a real-world app without addressing real-world issues like caching between app sessions, loss of network connectivity, multiple devices, etc doesn't really cut it, in my opinion.  I should have just created a simple app focusing on just the UI and basic MVVM architecture.  But I guess I was trying to show off.

So I put together my typical MVVM architecture based on [Matt Soucoup's version of ViewModel-first navigation](https://codemilltech.com/xamarin-forms-view-model-first-navigation/) that I've used on several significant projects.  And added the structure of typical data, caching, logging, and messaging services.  What I should have done was either just build a simple API to code and test against or added a mock data service that talked to a local SQLite DB.  However, I'd would avoid building a real app that required direct DB synchronization since that usually ends badly.

Instead, I had the bright idea of using the caching layer to mock a real data layer by pretending to cache the results of possible API calls as one would with [Akavache](https://github.com/reactiveui/Akavache).  Before too long, I realized my mistake as I was wound up re-creating the CRUD operations of a DB with the cache layer.  Still, I had to make what I had work enough for someone else to take a look.

In my notes for [the original branch](https://github.com/andylech/app-to-do-list-manager/tree/akavache), I mentioned that it would have been better if I had used [Rx.NET](https://github.com/dotnet/reactive) to accommodate multi-device synchronization and responsiveness.  As it happens, I attended a Meetup on 9/17/20 about [Reactive UI with Xamarin](https://www.meetup.com/Belgian-Mobile-NET-Developers-Group/events/269013859/) by Michael Stonis.  I had seen presentations for Rx.Net before.  But not being familiar with [ReactiveUI](https://www.reactiveui.net/), I wasn't aware that it supported the kind of ViewModel navigation I had used before through the [Sextant](https://github.com/reactiveui/Sextant) library as well.

Since I never heard back about my demo project and I felt it wasn't my best work by a long shot, I've decided to turn this project into an assignment for myself to learn ReactiveUI with Xamarin.Forms.  A to-do list manager app seem like a great use-case for this functionality and I now see the usefulness beyond the core Reactive Extensions.
