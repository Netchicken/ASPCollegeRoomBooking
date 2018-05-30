using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCollegeBooking.Data;

namespace ASPCollegeBooking.Data
{
    public class Seed
    {

        //        INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(1, 'D1', 'orange', 1,20)
        //INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(10, 'U10', 'orange', 1 ,15)
        //INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(11,'U11 EC Office', 'brown', 0 ,5)
        //INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(12, 'U12 Staff Cafe', 'brown', 0,5)
        //INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(13, 'U13 Coun Office', 'brown', 0,5)
        //        INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(14, 'U14 ', 'orange', 1,20)
        //        INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(15, 'U15', 'orange', 1,20)
        //        INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(16, 'U16', 'orange', 1,15)
        //        INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(17, 'U17', 'orange', 1,3 )
        //        INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(18, 'U18 PCopy', 'brown', 0,1)
        //        INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(19, 'U19', 'orange', 1,15)
        //        INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(2, 'D2 Interview', 'brown', 1,3)
        //        INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(20, 'U20', 'orange', 1,5)
        //        INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(21, 'U21', 'orange', 1,5)
        //        INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(3, 'D3', 'green', 1,15)
        //        INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(4, 'D4 Student Cafe', 'brown', 1,20)
        //        INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(5, 'D5 Student Kitchen', 'brown', 0,1)
        //        INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(6, 'D6 Office', 'red', 0,1)
        //        INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(7, 'D7 Office', 'red', 0,1)
        //        INSERT INTO[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES(8, 'U8', 'orange', 1,15)
        //        INSERT INTO.[Rooms]
        //                ([ID], [Title], [EventColor], [IsBookable] ,[MaxOccupancy]) VALUES(9, 'U9', 'orange', 1,15)



        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('1', 'D1', 'orange', 1,20)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('10', 'U10', 'orange', 1 ,15)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('11','U11 EC Office', 'brown', 0 ,5)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('12', 'U12 Staff Cafe', 'brown', 0,5)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('13', 'U13 Coun Office', 'brown', 0,5)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('14', 'U14 ', 'orange', 1,20)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('15', 'U15', 'orange', 1,20)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('16', 'U16', 'orange', 1,15)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('17', 'U17', 'orange', 1,3 )
        //INSERT INTO[dbo].[Rooms]
        //([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('18', 'U18 PCopy', 'brown', 0,1)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('19', 'U19', 'orange', 1,15)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('2', 'D2 Interview', 'brown', 1,3)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('20', 'U20', 'orange', 1,5)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('21', 'U21', 'orange', 1,5)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('3', 'D3', 'green', 1,15)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('4', 'D4 Student Cafe', 'brown', 1,20)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('5', 'D5 Student Kitchen', 'brown', 0,1)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('6', 'D6 Office', 'red', 0,1)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('7', 'D7 Office', 'red', 0,1)
        //INSERT INTO[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable],[MaxOccupancy]) VALUES('8', 'U8', 'orange', 1,15)
        //INSERT INTO.[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable] ,[MaxOccupancy]) VALUES('9', 'U9', 'orange', 1,15)






        //        INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'1', N'D1', N'orange', 1)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'10', N'U10', N'orange', 1)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'11', N'U11 EC Office', N'brown', 0)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'12', N'U12 Staff Cafe', N'brown', 0)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'13', N'U13 Coun Cafe', N'brown', 0)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'14', N'U14 ', N'orange', 1)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'15', N'U15', N'orange', 1)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'16', N'U16', N'orange', 1)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'17', N'U17', N'orange', 1)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'18', N'U18 PCopy', N'brown', 0)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'19', N'U19', N'orange', 1)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'2', N'D2 Interview', N'brown', 1)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'20', N'U20', N'orange', 1)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'21', N'U21', N'orange', 1)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'3', N'D3', N'green', 1)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'4', N'D4 Student Cafe', N'brown', 1)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'5', N'D5 Student Kitchen', N'brown', 0)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'6', N'D6 Office', N'red', 0)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'7', N'D7 Office', N'red', 0)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'8', N'U8', N'orange', 1)
        //INSERT INTO[dbo].[Rooms]
        //        ([ID], [Title], [EventColor], [IsBookable]) VALUES(N'9', N'U9', N'orange', 1)


    }
}
