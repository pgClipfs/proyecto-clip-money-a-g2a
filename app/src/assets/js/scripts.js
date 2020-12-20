// FUNCION PARA VALIDAR LOS INPUTS FILE(LAS FOTOS CARGADAS DE LA SELFIE, FRENTE Y DORSO DNI)
function validarExtension(id){
    idFoto = '';
 
    if (id == 0)
    {
        idFoto = 'fileselfie';
    }
    else if (id == 1)
    {
        idFoto = 'filefrente';
    }
    else if (id == 2)
    {
        idFoto = 'filedorso';
    }

    var archivoInput = document.getElementById(idFoto).value;
    var extPermitidas = /(.jpg|.png)$/i;

    if(!extPermitidas.exec(archivoInput))
    {
        
        return false;
    }
    else
    {
        return true;
    }

}