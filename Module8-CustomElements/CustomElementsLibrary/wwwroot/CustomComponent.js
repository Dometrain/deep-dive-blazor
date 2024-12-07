// Make sure that we can load _content files from the server where the custom element is hosted
var scriptUrl = document.currentScript.src;
var url = new URL(scriptUrl);
var customElementBaseUrl = url.origin;

const importMap = {
    imports: {
        [`${window.location.origin}/_content/`]: `${customElementBaseUrl}/_content/`,
    }
}

const importMapJson = JSON.stringify(importMap);
const scriptimportmap = document.createElement('script');
scriptimportmap.type = 'importmap';
scriptimportmap.textContent = importMapJson;
document.head.appendChild(scriptimportmap);

// Make sure that we can load the blazor.webassembly.js file from the server where the custom element is hosted
var blazorscript = document.createElement('script');
blazorscript.src = `${customElementBaseUrl}/_framework/blazor.webassembly.js`;
blazorscript.type = 'text/javascript';
blazorscript.setAttribute('AutoStart', 'false');
blazorscript.onload = function () {
    Blazor.start({
        loadBootResource: function (type, name, defaultUri, integrity) {
            if (type == 'dotnetjs') {
                return `${customElementBaseUrl}/_framework/${name}`;
            } else {
                return fetch(`${customElementBaseUrl}/_framework/${name}`, {
                    cache: 'no-cache',
                    integrity: integrity
                });
            }
        }
    });
};

document.head.appendChild(blazorscript);