using Assesssmenttest.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;

namespace Assesssmenttest.Steps
{
    [Binding]
    public sealed class TodoSteps
    {
        private HttpClient httpclient;
        private List<Todo> AllTodos;
        private Todo todo;
        [Given(@"I connected to the api")]
        public void GivenIConnectedToTheApi()
        {
            httpclient = HttpClientWrapper.HttpClientSetup();
        }

        [When(@"I make a request to All Todos")]
        public void WhenIMakeARequestToAllTodos()
        {
           var response = httpclient.GetAsync("todos").Result;
           var content = response.Content.ReadAsStringAsync().Result;
           AllTodos = JsonConvert.DeserializeObject<List<Todo>>(content);
        }

        [Then(@"I should get total count of (.*) Todos")]
        public void ThenIShouldGetTotalCountOfTodos(int count)
        {
            Assert.AreEqual(count, AllTodos.Count);
        }

        [When(@"I make a request for (.*) Todo")]
        public void WhenIMakeARequestForTodo(int id)
        {
           // var get = "todos/" + id;
            var response = httpclient.GetAsync("todos/"+id).Result;
            var content = response.Content.ReadAsStringAsync().Result;

            todo = JsonConvert.DeserializeObject<Todo>(content);

        }

        [Then(@"I should get title as '(.*)'")]
        public void ThenIShouldGetTitleAs(string title)
        {
            Assert.AreEqual(title, todo.title);
        }

        [When(@"I add a new todo to the list")]
        public void WhenIAddANewTodoToTheList()
        {
            var createtodo = new Todo()
            {
                userId = 1,
                title = "asif",
                completed = false

            };

            
            var serializecontent = JsonConvert.SerializeObject(createtodo);
            var response = httpclient.PostAsync("todos", new StringContent(serializecontent)).Result;
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
            todo = JsonConvert.DeserializeObject<Todo>(content);
        }

        [Then(@"the count should be updated to (.*)")]
        public void ThenTheCountShouldBeUpdatedTo(int totalcount)
        {
            Assert.AreEqual(totalcount, todo.id);
        }

    }
}
