# Tratamento-Global-Exception-Core
Projeto com exemplo de tratamento global de exceptions

Vamos criar um método de extensão para tratamento de exceptions 
```c#
  public static class ExceptionHandlerConfiguration
  {
      public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
      {
          app.UseExceptionHandler(builder =>
          {
              builder.Run(async ctx =>
              {
                  var errorApp = ctx.Features.Get<IExceptionHandlerFeature>();
                  var ex = errorApp.Error;

                  ctx.Response.StatusCode = (int)ex.GetStatusCode();
                  ctx.Response.ContentType = "application/json";

                  var success = false;
                  var message = ex.Message;
                  var messageType = ex.GetMessageType();

                  var strJson = $@"{{ ""sucess"": {success}, ""message"": ""{message}"", ""message_type"": ""{messageType}"" }}";
                  await ctx.Response.WriteAsync(strJson);
              });
          });
      }

      public static HttpStatusCode GetStatusCode(this Exception exception)
      {
          if (exception is IException exceptionResult)
              return exceptionResult.GetStatusCode();

          return HttpStatusCode.InternalServerError;
      }

      public static string GetMessageType(this Exception exception)
      {
          if (exception is IException exceptionResult)
              return exceptionResult.GetMessageType();

          return "error";
      }
  }
```

Chamar o método de extensão no Configure do Startup

```c#
  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  { 
      app.UseGlobalExceptionHandler();

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
          endpoints.MapControllers();
      });
  }
```

## Nossa API

```c#
  [HttpGet]
  public IEnumerable<Cliente> Get()
  {
      return this._db.Clientes;
  }

  [HttpGet("{id}")]
  public Cliente Get(int id)
  {
      var cliente = GetById(id);
      if (cliente == null)
          throw new ExceptionNotFound($"Cliente de Id {id} não localizado no sistema");

      return cliente;
  }

  [HttpPost]
  public void Post([FromBody] Cliente value)
  {
      value.Validar();

      var clienteResult = GetById(value.IdCliente);
      if (clienteResult != null)
          throw new ExceptionWarning("Cliente já existe com esse código no sistema");

      this._db.Clientes.Add(value);
  }

  [HttpPut("{id}")]
  public void Put(int id, [FromBody] Cliente value)
  {
      if (value.IdCliente != id)
          throw new ExceptionNotFound("Informe o ID Cliente igual ao ID da Url");

      value.Validar();

      var cliente = GetById(id);
      if (cliente == null)
          throw new ExceptionNotFound($"Cliente de Id {id} não localizado no sistema");

      cliente.NomeCliente = value.NomeCliente;
      cliente.DataNascimento = value.DataNascimento;
  }

  [HttpDelete("{id}")]
  public void Delete(int id)
  {
      var cliente = GetById(id);
      if (cliente == null)
          throw new ExceptionNotFound($"Cliente de Id {id} não localizado no sistema");

      this._db.Clientes.Remove(cliente);
  }
```

