    $(function () { // document ready
     $('#calendar').fullCalendar({
             now:  Date.now(), 
                    editable: true, // enable draggable events

                    //This registers the Drag and drop
                    eventDrop: function (event, delta, revertFunc) {
                       
                        //if (!confirm("Are you sure you want to change the " + event.title + " room booking?"))
                        //{ 
                        //    swal("Goodbye then, no changes have been made");
                        //    revertFunc();
                        //}
                        //else
                        //{
                         //   alert("Lets Do this!"),
                            //this isn't being triggered in create
                        // @Url.Action("PutEvents", "EventsApi", 5)
                        
                            $.ajax({
                                type: "PUT",
                                url: "/api/EventsAPI/" + event.id,
                                contentType: "application/json",
                                dataType: 'json',
                                data: JSON.stringify({
                                End: event.end,
                                    EventColor: event.EventColor,
                                    ResourceId: event.resourceId,
                                    ID: event.id,
                                    Start: event.start,
                                    Title: event.title,
                                    Email: event.email
                                }),
                                
                     success: function () {
                                    swal("You Beut! " + event.title + " has moved successfully");
                                },
                                failure: function (data) {
                                    alert("Ouch, It didn't work " + data.responseText);
                                    revertFunc();
                                },
                                error: function(data) {
                                    //alert(data.responseText),

                                    if (event.email === undefined) {
                                        swal("You have to be logged in the alter bookings ");
                                    } else {
                                        swal("Sorry, you can't move other peoples bookings.");
                                    }
                                    revertFunc();
                                }
                                }
                            );
                        // };
                        },
                    aspectRatio: 1.8,
                    scrollTime: '06:00', // undo default 6am scrollTime
                    header: {
                        left: 'today prev,next',
                        center: 'title',
                        right: 'timelineDay,timelineThreeDays,agendaWeek,month,listWeek'
                    },
                    defaultView: 'timelineDay',
                    views: {
                        timelineThreeDays: {
                            type: 'timeline',
                            duration: { days: 3 }
                        }
                    },
                    // resourceLabelText: 'Rooms',
                    resourceColumns: [
                        {
                            labelText: 'Room',
                            field: 'title'
                            //}, {
                            //    labelText: 'Bookable',
                            //    field: 'isbookable'
                            //
                        }],
                    //  resourceOrder: '-ID', // when occupancy tied, order by title
                    resources: {
                        type: "GET",
                        url: '/api/RoomsAPI',
                        error: function () {
                            $('#script-warning').show();
                        }
                    },

                    events: { // you can also specify a plain string like 'json/events.json'
                        url: '/api/EventsAPI',
                        error: function () {
                            $('#script-warning').show();
                        }
                       // eventOverlap: false // will cause the event to take up entire resource height
                        // eventOverlap: function (stillEvent, movingEvent) {
                        //    return stillEvent.allDay && movingEvent.allDay;
                        //  }

                    },

                    eventRender: function (event, element) {
                        element.qtip({
                            content: (
                                "<h5>" +
                                    "Start " +
                                    event.start.format("dddd MMMM YYYY") +
                                    "</h5>" +
                                    "<h5>" +
                                    "End " +
                                    event.end.format("dddd MMMM YYYY") +
                                    "</h5>" +
                                    "<h6>" +
                                    "Room " +
                                    event.resourceId +
                                    "</h6>" +
                                    "<h6>" +
                                    "Email " +
                                    event.email +
                                    "</h6>"
                            ),
                            position: {
                                my: "bottom left",
                                at: "right center",

                                viewport: $(window)
                                // my: 'top left'
                            },

                            show: {
                                effect: function() {
                                    $(this).fadeTo(500, 1);
                                }
                            },
                            hide: {
                                effect: function() {
                                    $(this).slideUp();
                                }
                            }
                            //style: {
                            //    classes: "qtip-bootstrap }"
                   

                        });
                    },

                    businessHours: {
                        // days of week. an array of zero-based day of week integers (0=Sunday)
                        dow: [1, 2, 3, 4, 5, 6, 0], // Monday - Fri
                        start: '6:00', // a start time (8am in this example)
                        end: '18:00' // an end time (6pm in this example)
                    }
                });
            });


