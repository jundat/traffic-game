

//------------------------------------------------------------------------------


var config = {
	width: 1200, 
	height: 640,
	params: { 
		enableDebugging:"0",
		backgroundcolor: "6f6f6f",
	    bordercolor: "ffffff",
	    textcolor: "FFFFFF",
	    logoimage: "https://dl.dropboxusercontent.com/u/41829250/KhoaLuan/DontDeleteData/MapEditor/MyLogo.png",
	    progressbarimage: "https://dl.dropboxusercontent.com/u/41829250/KhoaLuan/DontDeleteData/MapEditor/MyProgressBar.png",
	    progressframeimage: "https://dl.dropboxusercontent.com/u/41829250/KhoaLuan/DontDeleteData/MapEditor/MyProgressFrame.png"
	}
	
};
var u = new UnityObject2(config);

jQuery(function() {

	var $missingScreen = jQuery("#unityPlayer").find(".missing");
	var $brokenScreen = jQuery("#unityPlayer").find(".broken");
	$missingScreen.hide();
	$brokenScreen.hide();

	u.observeProgress(function (progress) {
		switch(progress.pluginStatus) {
			case "broken":
				$brokenScreen.find("a").click(function (e) {
					e.stopPropagation();
					e.preventDefault();
					u.installPlugin();
					return false;
				});
				$brokenScreen.show();
			break;
			case "missing":
				$missingScreen.find("a").click(function (e) {
					e.stopPropagation();
					e.preventDefault();
					u.installPlugin();
					return false;
				});
				$missingScreen.show();
			break;
			case "installed":
				$missingScreen.remove();
			break;
			case "first":
			break;
		}
	});
	u.initPlugin(jQuery("#unityPlayer")[0], "protected/game/game.unity3d");
});


//------------------------------------------------------------------------------

// Import JSON

function UploadJSONCallback(contents) {
	document.getElementById('popup').style.visibility = 'hidden';
	//document.getElementById('fileinput').style.visibility = 'hidden';
	document.getElementById('fileinput').removeEventListener('change', ReadSingleFile);

    u.getUnity().SendMessage("Menu", "OpenFileCallback2", contents);
}

function ReadSingleFile (evt) {
    var file = evt.target.files[0]; 
    if (file) {
      	var reader = new FileReader();
      	reader.onload = function(e) { 
	      	var contents = e.target.result;
	      	UploadJSONCallback (contents);
      	}
      	reader.readAsText(file);
    } else {
      	alert("Failed to load file");
    }
}

function ImportJSON (arg) {
	document.getElementById('popup').style.visibility = 'visible';
	//document.getElementById('fileinput').style.visibility = 'visible';
	document.getElementById('fileinput').value = null;
	document.getElementById('fileinput').addEventListener('change', ReadSingleFile);
}

function JSLog (arg) {
	alert(arg);
}


// Export JSON

function ExportJSON (arg) {
	var blob = new Blob([arg], {type: "text/plain;charset=utf-8"});
	saveAs(blob, "map.json");
}




