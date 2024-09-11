using PersonasAPI.Models;
using PersonasAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PersonasAPI.Tests
{
    public class PersonasControllerTests
    {
        [Fact]
        public async Task PostPersona_AgregaPersona_CuandoPersonaEsValido()
        {
            //Arrange
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);
            var nuevaPersona = new Persona 
            { 
                PrimerNombre = "Ana",
                SegundoNombre = "Celia",
                PrimerApellido = "de Armas", 
                SegundoApellido = "Caso",
                FechaNacimiento = new DateTime(1988, 4, 30),
                Dui = "01234567-8"
            };

            //Act
            var result = await controller.PostPersona(nuevaPersona);

            //Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var persona = Assert.IsType<Persona>(createdResult.Value);
            Assert.Equal("Ana", persona.PrimerNombre);
        }               

        [Fact]
        public async Task PostPersona_NoAgregaPersona_CuandoPrimeroNombreEsNulo()
        {
            //Arrange
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);
            var nuevaPersona = new Persona
            {
                PrimerNombre = null,
                SegundoNombre = "Margarita",
                PrimerApellido = "Vergara",
                SegundoApellido = "Vergara",
                FechaNacimiento = new DateTime(1972, 7, 10),
                Dui = "01234567-8"
            };

            //Act
            var result = await controller.PostPersona(nuevaPersona);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostPersona_AgregaPersona_CuandoSegundoNombreEsOpcional()
        {
            //Arrange
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);
            var nuevaPersona = new Persona
            {
                PrimerNombre = "Anya-Josephine",
                SegundoNombre = null,
                PrimerApellido = "Taylor-Joy",                
                SegundoApellido = null,
                FechaNacimiento = new DateTime(1996, 4, 16),
                Dui = "01234567-8"
            };

            //Act
            var result = await controller.PostPersona(nuevaPersona);

            //Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var persona = Assert.IsType<Persona>(createdResult.Value);
            Assert.Equal("Anya-Josephine", persona.PrimerNombre);
        }

        [Fact]
        public async Task PostPersona_NoAgregaPersona_CuandoNombresSuperaLimiteDeCaracteres()
        {
            //Arrange
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);
            var nuevaPersona = new Persona
            {
                PrimerNombre = "Nelzfexcndylveizrfofowfeoypznnqygvovhyyklbqozzlkxppdptkldipsimzniwkubdkgevesbmsaluxekjbhnovidtbbhhcehdjb",
                SegundoNombre = "Margarita",
                PrimerApellido = "Vergara",
                SegundoApellido = "Vergara",
                FechaNacimiento = new DateTime(1972, 7, 10),
                Dui = "01234567-8"
            };

            //Act
            var result = await controller.PostPersona(nuevaPersona);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostPersona_NoAgregaPersona_CuandoFechaNacimientoEsNulo()
        {
            //Arrange
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);
            var nuevaPersona = new Persona
            {
                PrimerNombre = "Sydney",
                SegundoNombre = "Bernice",
                PrimerApellido = "Sweeney",
                SegundoApellido = null,         
                FechaNacimiento = null,
                Dui = "01234567-8"
            };

            //Act
            var result = await controller.PostPersona(nuevaPersona);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostPersona_NoAgregaPersona_CuandoFechaNacimientoNoEsValido()
        {
            //Arrange
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);
            var nuevaPersona = new Persona
            {
                PrimerNombre = "Sydney",
                SegundoNombre = "Bernice",
                PrimerApellido = "Sweeney",
                SegundoApellido = null,
                FechaNacimiento = new DateTime(2030,5,15),
                Dui = "01234567-8"
            };

            //Act
            var result = await controller.PostPersona(nuevaPersona);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostPersona_NoAgregaPersona_CuandoDuiNoEsValido()
        {
            //Arrange
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new PersonasController(context);
            var nuevaPersona = new Persona
            {
                PrimerNombre = "Sydney",
                SegundoNombre = "Bernice",
                PrimerApellido = "Sweeney",
                SegundoApellido = null,
                FechaNacimiento = new DateTime(1997, 9, 12),
                Dui = "123-3"
            };

            //Act
            var result = await controller.PostPersona(nuevaPersona);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
