using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Xml.Linq;
using BGU_SE_Courses.Kanban.Backend.ServiceLayer;

namespace BGU_SE_Courses.Kanban.Backend.ServiceLayer

public class Tasks
{
    private readonly Boardfacad boardFacad;
    private readonly UserFacad userfacad;
    public Tasks(Boardfacad BoardFacad)
    {
        this.Boardfacad = boardFacad;

    }




    public string PlayingTasks(string email)
    {
        Response response;

        try
        {
            List<Task> inprogressTasks = Boardfacad.InProgessTasks(email);
            Task[] tasksArray = inprogressTasks.ToArray();
            response = new Response(null, tasksArray);
            return JsonSerializer.Serialize(response);
        }
        catch (Exception e)
        {
            response = new Response(e.Message, null);
            return JsonSerializer.Serialize(response);
        }


    }

    public string ChangeTaskPlace(string email, string boardName, int columnOrdinal, int taskId)
    {
        Response response; 
        try
        {
            Boardfacad.ChangeTaskPlace(email, boardName, columnOrdinal, taskId);
            response = new Response();
            return JsonSerializer.Serialize(response);
        }
        catch (Exception e)
        {
            response = new Response(e.Message, null);
            return JsonSerializer.Serialize(response);
        }

    }


    public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
    {
        Response response;
        try
        {
            Boardfacad.AddTask(email, boardName, title, description, dueDate);
            response = new Response();
            return JsonSerializer.Serialize(response);
        }
        catch (Exception e)
        {
            response = new Response(e.Message, null);
            return JsonSerializer.Serialize(response);
        }
        public string UpdateTaskDateDue(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            Response response;
            try
            {
                Boardfacad.UpdateTaskDueDate(email, boardName, columnOrdinal, taskId, dueDate);
                response = new Response();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception e)
            {
                response = new Response(e.Message, null);
                return JsonSerializer.Serialize(response);
            }

        }

        public string ChangeTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            Response response;
            try
            {
                Boardfacad.UpdateTaskTitle(email, boardName, columnOrdinal, taskId, title);
                response = new Response();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception e)
            {
                response = new Response(e.Message, null);
                return JsonSerializer.Serialize(response);
            }
        }
        public string ChangeTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            Response response 
            try
            {
                Boardfacad.UpdateTaskTitle(email, boardName, columnOrdinal, taskId, title);
                response = new Response();

                return JsonSerializer.Serialize(response);
            }
            catch (Exception e)
            {
                response = new Response(e.Message, null);
                return JsonSerializer.Serialize(response);
            }
        }



    }







}
