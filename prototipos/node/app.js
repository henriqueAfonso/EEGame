//Imports
const express = require('express');
var keypress = require('keypress');

const app = express();
keypress(process.stdin);


//Rotas
app.get('/', (req, res) => {
	    res.send('Raíz da api');
})

app.get('/controls', (req, res) => {
    res.send({
    	"jumpDown": (controls.jumpUp != jumpAux) && (controls.jumpUp),
    	"jumpUp": (!controls.jumpUp) && (jumpAux),
    	"horizontal": controls.horizontal
    });
    jumpAux = controls.jumpUp;
    if(Date.now()-anteriorJump > 300){
	    controls.jumpUp = false;
	    controls.jumpDown = true;
	    setTimeout(() => controls.jumpDown = false, 3000);
	}
	if(Date.now()-anteriorHori > 300){
	    controls.horizontal = 0;
	}
});


//Controles
let controls = {
    "jumpUp":    false,
    "jumpDown":  false,
    "horizontal":    0
}
let jumpAux = false;
let anteriorJump = Date.now();
let anteriorHori = anteriorJump;
process.stdin.on('keypress', function (ch, key) {
  if(key.name == 'u'){
  	controls.jumpUp = true;
  	anteriorJump = Date.now();
  }
  if(key.name == 'k'){
  	controls.horizontal = 1;
  	anteriorHori = Date.now();
  }
  if(key.name == 'h'){
  	controls.horizontal = -1;
  	anteriorHori = Date.now();
  }
  console.log(controls);
});
 
process.stdin.setRawMode(true);
process.stdin.resume();


//Rodar API
app.listen(3000, () => console.log('Ouvindo porta 3000'));