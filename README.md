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

### API Design

* No live API was provided, nor was an API design or interface specified.  I chose to anticipate a REST API that could accomodate local NoSQL-style caching with [Akavache](https://github.com/reactiveui/Akavache) in a SQLite DB.  This approach was chosen to allow the API to be the "source of truth" but allow for synchronization across multiple devices.  Specifically, it avoids the issue of distributed DBs keeping distinct copies that need to be synchronized while likely downloading the same static data multiple times.
* Further, retaining a temporary copy of the user's data allows for interaction in a disconnected state as often happens with mobile devices.
* ~~In my mind, the API will be more of a "point-in-time" type architecture where new entries will be added to replace old ones when list items are updated.  Because the collection of ids attached to a list will also be updated, this allows for a transactional history of modifications to a single list or across lists.  This would be easier to implement in a DB behind an API than in a flat SQLite DB for local cache, but it at least allows for the flexibility.~~
* After further thought and testing of [Todoist](https://todoist.com) on multiple devices with the same account, I decided to use a simpler solution of "last update wins" when more than one device submits on update at nearly the same time.  Each change winds up in the log, but only the last update will be stored and returned on list refresh.  I knew the other solution was going to use more items as it cloned old items on updates, so this solution also avoids extra cleanup.

---

### Dependencies

* [FancyLogger](https://github.com/xamarinfiles/library-fancy-logger): My own NuGet for formatting output, particularly to DEBUG, so it won't be lost in the clutter of Android logs in particular.  Also, works well with [VSColorOutput](https://mike-ward.net/vscoloroutput/).
* [Akavache](https://github.com/reactiveui/Akavache): Asynchronous, persistent key-value based on SQLite.
* **TODO**

---

### Caveats/Notes

* I could easily have used an IoC container to handle constructor injection, replace the ServiceManager and make other improvements.  But I was trying not to overdue the dependencies of this simple demo project.
* The messaging service used here is a simple static one for displaying exceptions.  A full-blown app could, of course, use Xamarin.Forms MessagingCenter for things like analytics reporting, displaying alerts when not convenient (PageModels), etc.
* When consuming a real REST API, I typically use [Refit](https://github.com/reactiveui/refit) to avoid most of the HttpClient boilerplate code.
* A real multi-device to-do list manager would probably use some form of [Reactive Extensions (Rx)](https://github.com/dotnet/reactive) to handle pushing changes to devices instead of polling.

---

### Conclusion

9/15/2020: This is still very much a work-in-progress at the moment with the data architecture operational to fake a real API using Akavache to do client-side caching of a web service without actually being able to use a real web service or commit to the schema of its endpoints.  The app architecture, navigation, data services, ViewModels (PageModels), a basic error-reporting schema, etc. are all the there.

What I haven't finished yet is the UI presentation and functionlity.  The list items display, but the interaction I was working on tonight isn't finished.  The page to edit lists is there, but the lists don't load into the view yet because of any issue with the data type I used to capture the top-level list info.  Once I change the list-header data structure, I can apply similar changes to the list-management view as I was working on for the list-item-management view.


The requirement of the assignment was not actually to create the data architecture and I have a feeling I could have created an app using only in-memory data.  But that wouldn't have felt like a real app to me as it wouldn't retain data across instances.  The data architecture decision are one of the most important in an app (maybe not the first one, but usually in the top five) and I generally work in a bottom-up, data-first approach to building my apps that it would have felt very odd not to work up with even a temporary foundation.

Anyway,I guess all my experience in mobile development actually got in my way this time and led to this not being my best work.  Which is disappointing, but that's just the way it goes sometimes.

I will wrap up the remaining app in its current form in the next day or so. I feel like redoing this project with a real API of some sort which would've allowed better decisions upfront.  But the reality is that the world doesn't need another to-do list manager, so I should spend my time on other projects in the future.


