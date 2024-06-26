﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ConsultaAlumnos.Domain.Entities;
using ConsultaAlumnos.Domain.Exceptions;
using ConsultaAlumnos.Application.Interfaces;
using ConsultaAlumnos.Application.Models;
using ConsultaAlumnos.Application.Services;
using ConsultaAlumnos.Application.Models.Requests;

namespace ConsultaAlumnos.Web;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost("[action]")]
    public IActionResult Create([FromBody] StudentCreateRequest studentCreateRequest)
    {
        try
        {
            var obj = _studentService.Create(studentCreateRequest);
            return CreatedAtAction(nameof(GetByID), new {id = obj.Id}, obj);
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<StudentDto> GetByID([FromRoute] int id)
    {
        try
        {
            return Ok(_studentService.GetById(id));
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] StudentUpdateRequest studentUpdateRequest)
    {
        try
        {
            _studentService.Update(id, studentUpdateRequest);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        try
        {
            _studentService.Delete(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("[Action]")]
    public ActionResult<List<StudentDto>> GetAll()
    {
        return Ok(_studentService.GetAll());
    }

    [HttpGet("[Action]")]
    public List<Student> GetAllFullData()
    {
        return _studentService.GetAllFullData();
    }

}
