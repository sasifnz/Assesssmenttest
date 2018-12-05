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
        
        [TestMethod]
        public void Getbyid()
        {

            var httpclient = HttpClientWrapper.HttpClientSetup();
            var response = httpclient.GetAsync("todos/1").Result;
            var content = response.Content.ReadAsStringAsync().Result;
                        
            var todo = JsonConvert.DeserializeObject<Todo>(content);
            Assert.AreEqual(1, todo.userId);
            Assert.AreEqual(1, todo.id);
            Assert.AreEqual("delectus aut autem", todo.title);
            Console.WriteLine(todo.title);
            Assert.AreEqual(false, todo.completed);
        }
      
        [TestMethod]
        public void GetTodos()
        {
            var httpclient = HttpClientWrapper.HttpClientSetup();
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
         
            var httpclient = HttpClientWrapper.HttpClientSetup();
            var serializecontent = JsonConvert.SerializeObject(todo);
            var response = httpclient.PostAsync("todos", new StringContent(serializecontent)).Result;
            //var content = response.Content.ReadAsStringAsync().Result;
            // var PostTodo = JsonConvert.DeserializeObject<List<Todo>>(content);//
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
            var insertedtodo = JsonConvert.DeserializeObject<Todo>(content);
                        
        }
        [TestMethod]
        public void PutTodo()
        {
            var todo = new Todo()
            {
                userId = 10,
                id = 200,
                title = "asif1",
                completed = false

            };

            var httpclient = HttpClientWrapper.HttpClientSetup();
            var serializecontent = JsonConvert.SerializeObject(todo);
            var response = httpclient.PutAsync("todos/200", new StringContent(serializecontent)).Result;
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
            var updatedtodo = JsonConvert.DeserializeObject<Todo>(content);
            Assert.AreEqual("asif1", updatedtodo.title);

        }
    }
}
