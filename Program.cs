// See https://aka.ms/new-console-template for more information

using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using CS_First_HTTP_Client;

ScheduleService scheduleService = new(ApiService.Current);

var schedule = await scheduleService.GetAcademicSchedule();
    
    await ApiService.Current.AuthenticateAsync (new Login( "jinseo.kim@winsor.edu", "BTNrwo900%&!"));
    
    var user = await ApiService.Current.SendAsync<UserInfo>(HttpMethod.Get, "api/users/self");
    Console.WriteLine (user);

    var cycleDays = await ApiService.Current.SendAsync<CycleDay[]>(HttpMethod.Get, "api/schedule/cycle-day");

    foreach (var day in cycleDays)
    {
        Console.WriteLine($"{day.date:dddd dd MMMM} is {day.cycleDay}");
    }
    