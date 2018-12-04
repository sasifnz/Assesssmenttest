using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Assesssmenttest.Models;
using System.Collections.Generic;

namespace Assesssmenttest
{
    [TestClass]
    public class UnitTest1
    {
        private string _apiBaseUrl = "https://jsonplaceholder.typicode.com/";
        [TestMethod]
        public void Getbyid()
        {
            var httpclient = HttpClientSetup();
           // httpclient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            /* https://jsonplaceholder.typicode.com/ */
            var response = httpclient.GetAsync("todos/1").Result;
            var content = response.Content.ReadAsStringAsync().Result;

            var todo = JsonConvert.DeserializeObject<Todo>(content);
            Assert.AreEqual(1, todo.userId);
            Assert.AreEqual(1, todo.id);
            Assert.AreEqual("delectus aut autem", todo.title);
            Assert.AreEqual(false, todo.completed);
        }
        private HttpClient HttpClientSetup()
        {
            var client = new HttpClient { BaseAddress = new Uri(_apiBaseUrl) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = _authenticationHeader;

            return client;
        }
        [TestMethod]
        public void GetTodos()
        {
            var httpclient = HttpClientSetup();
            var response = httpclient.GetAsync("todos").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var todos = JsonConvert.DeserializeObject<List<Todo>>(content);

            Assert.AreEqual(200, todos.Count);
        }
        [TestMethod]
        public void PostTodo()
        {
            var todo = new Todo()
            {
                userId = 1, title = "asif", completed = false
            
            };
         
            var httpclient = HttpClientSetup();
            var serializecontent = JsonConvert.SerializeObject(todo);
            var response = httpclient.PostAsync("todos", new StringContent(serializecontent)).Result;
            //var content = response.Content.ReadAsStringAsync().Result;
            // var PostTodo = JsonConvert.DeserializeObject<List<Todo>>(content);//
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var content = response.Content.ReadAsStringAsync().Result;
            var insertedtodo = JsonConvert.DeserializeObject<Todo>(content);
        }
    }
}
