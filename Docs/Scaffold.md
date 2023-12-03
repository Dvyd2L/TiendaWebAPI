# Scaffold

Scaffold-DbContext "Data Source=<BDServer>;Initial Catalog=<BDName>;Integrated Security=<Boolean>;TrustServerCertificate=<Boolean>" -Provider <BDEngine> -OutputDir <outFolder> -force -project <ProjectName>

Scaffold-DbContext "Data Source=localhost;Initial Catalog=<NombreBaseDatos>;Integrated Security=True;TrustServerCertificate=True" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force -project <NombreProyecto>

Scaffold-DbContext "Data Source=PC_CELIA_DAVID;Initial Catalog=MiTienda;Integrated Security=True;TrustServerCertificate=True" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force -project TiendaWebAPI
