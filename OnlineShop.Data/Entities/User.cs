﻿using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Data.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; }
}