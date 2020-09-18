# To-Do List Manager
A sample to-do list manager in Xamarin.Forms showing off ViewModel navigation and other features in Reactive UI

I did this originally as an coding assignment for a potential job.  The assignment was to create a real-world example of an to-do list app for multiple lists but without specifying any particular data architecture.  I generally work data-first, bottom-up and creating a real-world app without addressing real-world issues like caching between app sessions, loss of network connectivity, multiple devices, etc doesn't really cut it, in my opinion.  I should have just created a simple app focusing on just the UI and basic MVVM architecture.  But I guess I was trying to show off.

Well, I put together my typical version of MVVM architecture based on [Matt Soucoup's version of ViewModel-first navigation](https://codemilltech.com/xamarin-forms-view-model-first-navigation/) that I've used on several significant projects.  And added the structure of typical data, caching, logging, and messaging services.  What I should have done was either just build a simple API to code and test against or add a mock data service that talked to a local SQLite DB.  Instead, I had the bright idea of using the caching layer to mock a real data layer by pretending to cache the results of possible API calls as one would with [Akavache](https://github.com/reactiveui/Akavache).  Of course, before too long, I realized my mistake as I was wound recreating the CRUD operations of a DB with the cache layer, but I had to finish what I had to make it work enough for someone else to take a look.

In my notes for [the original branch](https://github.com/andylech/app-to-do-list-manager/tree/akavache), I pointed out early on that it probably would have been if I had used [Rx.NET](https://github.com/dotnet/reactive).  But I had never built anything with Reactive Extensions before so it would have been inadvisable to try for this demo project.  As it happens, I had previously signed up for a Meetup today on [Reactive UI with Xamarin](https://www.meetup.com/Belgian-Mobile-NET-Developers-Group/events/269013859/) by Michael Stonis.  Having seen presentations for Rx.Net but not ReactiveUI before, I wasn't aware that [ReactiveUI](https://www.reactiveui.net/) accommodates the essential ViewModel navigation I use as well as providing a greater possibility of multi-device synchronization that one would find in a real-world to-do list used across multiple devices.

Since I never heard back about my demo project and I felt it wasn't my best work by a long shot, I've decided to turn this project into an assignment for myself to learn ReactiveUI since to-do lists seem like a great use-case for this functionality and I now see the usefulness beyond the core Reactive Extensions.

More to follow.  I will be re-posting the GitKraken link once I update the project board for the new version.
