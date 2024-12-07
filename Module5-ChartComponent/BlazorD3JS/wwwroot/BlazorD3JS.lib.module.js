 export function beforeWebStart() {
     var d3js = document.createElement('script');
    d3js.setAttribute('src', 'https://d3js.org/d3.v7.min.js')
    document.head.appendChild(d3js);
}