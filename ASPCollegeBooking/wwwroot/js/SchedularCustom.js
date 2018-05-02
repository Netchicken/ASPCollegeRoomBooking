    $(function () { // document ready
     $('#calendar').fullCalendar({
             now:  Date(), //'2017-10-07',
                    editable: true, // enable draggable events

                    //This registers the Drag and drop
                    eventDrop: function (event, delta, revertFunc) {
                        //alert(event.title +
                        //    "\n" + moment().format('MMMM Do YYYY, h:mm:ss a') + " was dropped  " +
                        //    "\n Start " + event.start.format("dddd MMMM YYYY") +
                        //    "\n End " + event.end.format("dddd MMMM YYYY") +
                        //    "\n Room id " + event.resourceId +
                        //    "\n Row id " + event.id

                        //);

                        if (!confirm("Are you sure you want to change the room booking?")) {
                            alert("Goodbye then");
                            revertFunc();
                        } else {
                          //  alert("Lets Do this!"),
                            //stupid date formatting
                            // var formatStart = event.start.format("dd/MM/YYYY"),
                            //     var dateOut = $(event.start.format("dd/MM/YYYY")).val().split("/"),
                            //     var StartDateToSend = new Date(dateOut[1] + "/" + dateOut[0] + "/" + dateOut[2]),


                            $.ajax({
                                type: "PUT",
                                url: "api/EventsAPI/" + event.id,
                                contentType: "application/json",
                                dataType: 'json',
                                data: JSON.stringify({
                                    //End: "2017-12-18T14:23:00",
                                    //EventColor: "Green",
                                    //ResourceId: "3",
                                    //ID: "f12afa3f-ba1a-44fc-9d31-de2773e70c98",
                                    //Start: "2017-11-16T14:32:00",
                                    //Title: "Thursday angst"
                                    End: event.end,
                                    EventColor: event.EventColor,
                                    ResourceId: event.resourceId,
                                    ID: event.id,
                                    Start: event.start,
                                    Title: event.title
                                }),
                                //    data: JSON.stringify(uploadevents),

                                // $(event.start).val,//.format("dd/MM/YYYY HH:MM:SS ")).val(),
                                //$(event.end).val(),
                                //    Start: $("18/11/2017 2:23:00 PM"),
                                //    End: $("18/12/2017 2:23:00 PM"),


                                success: function () {
                                    alert("You Beut!");
                                },
                                failure: function (data) {
                                    alert(data.responseText);
                                },
                                error: function (data) {
                                    alert(data.responseText),
                                        alert("That Sucks!");
                                }

                                //        data: {
                                //           ID: $(event.id).val(),
                                //            ResourceId: $(event.resourceId).val(),
                                //            EventColor: $("").val(),
                                //            //17/11/2017 1:58:00 PM
                                //            Start: $(event.start.format("dd/MM/YYYY HH:MM:SS ")).val(),
                                //            End: $(event.end).val(),
                                //            Title: $("").val()


                                //}
                            });

                        };


                    },
                    aspectRatio: 1.8,
                    scrollTime: '08:00', // undo default 6am scrollTime
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
                            content: (event.title +
                                "\n Start " + event.start.format("dddd MMMM YYYY") +
                                "\n End " + event.end.format("dddd MMMM YYYY") +
                                "\n Room id " + event.resourceId +
                                "\n Row id " + event.id
                            ),
                            position: {
                                //adjust: {
                                //    method: 'flipinvert'
                                //},
                                viewport: $(window)
                                // my: 'top left'
                            }
                        });
                    },

                    businessHours: {
                        // days of week. an array of zero-based day of week integers (0=Sunday)
                        dow: [1, 2, 3, 4, 5], // Monday - Fri
                        start: '8:00', // a start time (8am in this example)
                        end: '18:00' // an end time (6pm in this example)
                    }
                });
            });


