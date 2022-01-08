using DemoTransaction.API.Application.DTO;
using DemoTransaction.API.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MOP.Order.API.Controllers;

[Produces("application/json")]
[Route("api/orders")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IOrderServiceUoW _orderServiceUoW;
    private readonly IOrderServiceTransactionScope _orderServiceTransactionScope;

    public OrdersController(IOrderService orderService, IOrderServiceUoW orderServiceUoW, IOrderServiceTransactionScope orderServiceTransactionScope)
    {
        _orderService = orderService;
        _orderServiceUoW = orderServiceUoW;
        _orderServiceTransactionScope = orderServiceTransactionScope;
    }

    // POST api/orders/normal-transaction
    /// <summary>
    /// EF Controlando Transações
    /// </summary>   
    /// <remarks>
    /// Exemplo request:
    ///
    ///     POST /Order
    ///     {  
    ///         "shipping": 1,
    ///         "observation": "Cliente irá retirar na loja",
    ///         "customer": {
    ///             "name": "Cliente Demonstração",
    ///             "phone": "51999123456",
    ///             "email": "teste@gamil.com",
    ///             "cep": "99900000",
    ///             "state": "Porto Alegre",
    ///             "city": "RS"
    ///         },
    ///         "items": [
    ///             {
    ///                 "productId": 1,
    ///                 "quantity": 2,
    ///                 "unitPrice": 450.00,
    ///                 "discount": 0,
    ///                 "discountValue": 200.00
    ///             }
    ///         ]
    ///     }
    /// </remarks>        
    /// <returns>Retorna objeto criado da classe Pedido</returns>                
    /// <response code="201">O pedido foi incluído corretamente</response>                
    /// <response code="400">Falha na requisição</response>         
    [HttpPost("normal-transaction")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ActionName("NewOrder")]
    public async Task<IActionResult> PostNormalAsync([FromBody] OrderInputModel inputModel)
    {
        var id = await _orderService.CreateAsync(inputModel);

        return CreatedAtAction("NewOrder", new { id = id }, inputModel);

    }

    // POST api/orders/manual-transaction
    /// <summary>
    /// Transação Manual
    /// </summary>   
    /// <remarks>
    /// Exemplo request:
    ///
    ///     POST /Order
    ///     {  
    ///         "shipping": 1,
    ///         "observation": "Cliente irá retirar na loja",
    ///         "customer": {
    ///             "name": "Cliente Demonstração",
    ///             "phone": "51999123456",
    ///             "email": "teste@gamil.com",
    ///             "cep": "99900000",
    ///             "state": "Porto Alegre",
    ///             "city": "RS"
    ///         },
    ///         "items": [
    ///             {
    ///                 "productId": 1,
    ///                 "quantity": 2,
    ///                 "unitPrice": 450.00,
    ///                 "discount": 0,
    ///                 "discountValue": 200.00
    ///             }
    ///         ]
    ///     }
    /// </remarks>        
    /// <returns>Retorna objeto criado da classe Pedido</returns>                
    /// <response code="201">O pedido foi incluído corretamente</response>                
    /// <response code="400">Falha na requisição</response>         
    [HttpPost("manual-transaction")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ActionName("NewOrder")]
    public async Task<IActionResult> PostManualAsync([FromBody] OrderInputModel inputModel)
    {
        var id = await _orderServiceUoW.CreateAsync(inputModel);

        return CreatedAtAction("NewOrder", new { id = id }, inputModel);
    }

    /// <summary>
    /// Utilizando Transaction Scoped
    /// </summary>   
    /// <remarks>
    /// Exemplo request:
    ///
    ///     POST /Order
    ///     {  
    ///         "shipping": 1,
    ///         "observation": "Cliente irá retirar na loja",
    ///         "customer": {
    ///             "name": "Cliente Demonstração",
    ///             "phone": "51999123456",
    ///             "email": "teste@gamil.com",
    ///             "cep": "99900000",
    ///             "state": "Porto Alegre",
    ///             "city": "RS"
    ///         },
    ///         "items": [
    ///             {
    ///                 "productId": 1,
    ///                 "quantity": 2,
    ///                 "unitPrice": 450.00,
    ///                 "discount": 0,
    ///                 "discountValue": 200.00
    ///             }
    ///         ]
    ///     }
    /// </remarks>        
    /// <returns>Retorna objeto criado da classe Pedido</returns>                
    /// <response code="201">O pedido foi incluído corretamente</response>                
    /// <response code="400">Falha na requisição</response>         
    [HttpPost("transaction-scoped")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ActionName("NewOrder")]
    public async Task<IActionResult> PostTransactionScopeAsync([FromBody] OrderInputModel inputModel)
    {
        var id = await _orderServiceTransactionScope.CreateAsync(inputModel);

        return CreatedAtAction("NewOrder", new { id = id }, inputModel);
    }



}

