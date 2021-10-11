﻿using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Service;
using ApiCatalogoJogos.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Controllers.V1

{


    [Route("api/v1/[controller]")]

    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _serviceJogo;

        public JogosController(IJogoService serviceJogo)
        {
            _serviceJogo = serviceJogo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await _serviceJogo.Obter(pagina, quantidade);

            if (jogos.Count() == 0)
                return NoContent();

            return Ok(jogos);
        }

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo)
        {
            var jogo = await _serviceJogo.Obter(idJogo);

            if (jogo == null)
                return NoContent();

            return Ok(jogo);
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = await _serviceJogo.Inserir(jogoInputModel);

                return Ok(jogo);
            }
            //catch (JogoJaCadastradoException ex)
            catch(Exception ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }


        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo(Guid idJogo, JogoInputModel Jogo)
        {
            return Ok();
        }
        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _serviceJogo.Atualizar(idJogo, preco);

                return Ok();
            }
            catch (JogoNaoCadastrado ex)
        
            {
                return NotFound("Não existe este jogo");
            }
        }


        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> DeletarJogo(Guid idJogo)
        {
            return Ok();
        }
        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await _serviceJogo.Remover(idJogo);

                return Ok();
            }
            catch (JogoNaoCadastrado ex)
            
            {
                return NotFound("Não existe este jogo");
            }
        }
    }
}
