﻿using AllinOne.DataHandlers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TrackerUIWeb.Data.DataTransferObjects;
using static TrackerUIWeb.Pages.Login;

namespace TrackerUIWeb.Data.ApiGateway
{
    public class HttpHandlerService
    {
        private readonly HttpClient _httpClient;

        public HttpHandlerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<T>> GetList<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}{endpoint}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(content);
            }
            return null;
        }


        public async Task<T> Get<T>(string endpoint)
        {
            try
            {
                endpoint = $"{_httpClient.BaseAddress}{endpoint}";
                var response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return default;
        }



        public async Task<OutputHandler> Create<T>(string endpoint, T param)
        {
            try
            {
                endpoint = $"{_httpClient.BaseAddress}{endpoint}";

                //if (typeof(T) == typeof(LoginModel))
                //{
                //    endpoint = endpoint.Replace("api/","");
                //    endpoint = $"{endpoint}?useCookies=false&useSessionCookies=false";

                //}

                HttpContent content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await _httpClient.PostAsJsonAsync<T>(endpoint, param);

               
              var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<OutputHandler>(result);
                 

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<T> Get<T>(string endpoint, T param)
        {
            try
            {
                endpoint = $"{_httpClient.BaseAddress}{endpoint}";

                HttpContent content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await _httpClient.PostAsJsonAsync<T>(endpoint, param);


                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<T> GetSingle<T>(string endpoint, ProductSearchDTO param)
        {
            try
            {
                endpoint = $"{_httpClient.BaseAddress}{endpoint}";

                HttpContent content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await _httpClient.PostAsJsonAsync<ProductSearchDTO>(endpoint, param);


                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> GetSingleItem<T>(string endpoint, T param)
        {
            try
            {
                endpoint = $"{_httpClient.BaseAddress}{endpoint}";

                HttpContent content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await _httpClient.PostAsJsonAsync<T>(endpoint, param);


                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OutputHandler> Delete(string endpoint)
        {
            try
            {
                endpoint = $"{_httpClient.BaseAddress}{endpoint}";

                var response = await _httpClient.DeleteAsync($"{endpoint}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<OutputHandler>(result);
                }
                throw new Exception();
                //  return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<OutputHandler> Delete<T>(string endpoint, T param)
        {
            try
            {
                endpoint = $"{_httpClient.BaseAddress}{endpoint}";

                var response = await _httpClient.PostAsJsonAsync($"{endpoint}",param);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<OutputHandler>(result);
                }
                throw new Exception();
                //  return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OutputHandler> Update<T>(string endpoint, T param)
        {
            try
            {
                endpoint = $"{_httpClient.BaseAddress}{endpoint}";

                var response = await _httpClient.PutAsJsonAsync<T>(endpoint, param);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<OutputHandler>(result);
                }
                throw new Exception(response.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
