
function contarCodigo(codigo) {

  let lineas = codigo.split('\n').length;
  

  let variables = codigo.match(/\b(let|const|var)\s+\w+/g);
  let numVariables = variables ? variables.length : 0;

  
  let funcionesNormales = codigo.match(/function\s+\w+/g);
  let funcionesFlechas = codigo.match(/(let|const|var)\s+\w+\s*=\s*\([^)]*\)\s*=>/g);
  


  let numFunciones = 0;
  if (funcionesNormales) numFunciones += funcionesNormales.length;
  if (funcionesFlechas) numFunciones += funcionesFlechas.length;



  console.log('Total de líneas: ' + lineas);
  console.log('Variables: ' + numVariables);
  console.log('Funciones: ' + numFunciones);
}

// Prueba
let miCodigo = `let nombre = "Juan";
const edad = 25;
let ciudad = "Madrid";

function saludar() {
  console.log("Hola");
}

const sumar = (a, b) => {
  return a + b;
}`;

contarCodigo(miCodigo);