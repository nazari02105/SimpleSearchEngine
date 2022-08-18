function getLastWord() {
    const words = document.getElementById("search").value.split(' ');
    let lastWord = words[words.length - 1];
    if (lastWord.startsWith('+') || lastWord.startsWith('-'))
        lastWord = lastWord.substr(1);
    return lastWord;
}

function validate (){
    document.getElementById("searchError").innerHTML = "";

    let status = true;

    if (document.getElementById("search").value == ""){
        document.getElementById("searchError").innerHTML = "Phase Can Not Be Null";
		document.getElementById("searchError").style.color = "red";
        document.getElementById("searchError").style.fontSize = "14px";
        status = false;
    }
    else {
        let lastWord = getLastWord();
        if (lastWord.length > 2) {
            const xmlhttp = new XMLHttpRequest();
            xmlhttp.responseType = 'json';
            xmlhttp.onload = function () {
                document.getElementById("searchError").style.color = "blue";
                document.getElementById("searchError").style.fontSize = "14px";
                const res = this.response;
                for (let i = 0; i < res.length; ++i) {
                    document.getElementById("searchError").innerHTML += res[i] + "<br>";
                    if (i >= 10) break;
                }
            }
            xmlhttp.open("GET", "https://localhost:5001/Search/GetHints?hint=" + lastWord);
            xmlhttp.send();
        }
    }
}

function clean (){
    document.getElementById("searchError").innerHTML = "";
}

function changePage123 (){
    document.getElementById("queryForm").submit();
    //window.open("https://localhost:5001/ChangePage/Query?query=" + document.getElementById('search').value,"_self");
}