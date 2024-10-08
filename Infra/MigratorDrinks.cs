﻿using FluentMigrator;
using System;

namespace Infra
{
    [Migration(20240826203040)] 

public  class MigratorDrinks : Migration
{   
        public override void Up()
        {
            Create.Table("Drink")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()// identity é ser incrementavel
                .WithColumn("Nome").AsString()
                .WithColumn("Preco").AsDouble()
                .WithColumn("Existe").AsBoolean()
                .WithColumn("EhAlcoolica").AsBoolean()
                .WithColumn("Composicao").AsInt16();
        }
        public override void Down()
        {
            throw new NotImplementedException();
        }
    }

}

