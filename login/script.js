<script>
function verify {
	var xmlhttp = new XMLHttpRequest();
	xmlhttp.onreadystatechange = function() {
		if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
			if(xmlhttp.responseText == "ok"){
				document.getElementById("Button1").Enabled = True;
			}
		}
	}
	xmlhttp.open("POST", "verify.py", true);
	xmlhttp.send();
}
</scirpt>
<script src="//codecha.org/api/challenge?k=f6fe6b70cad949acb6fa15b4e9e381aa&re_k=f6fe6b70cad949acb6fa15b4e9e381aa"> </script>
