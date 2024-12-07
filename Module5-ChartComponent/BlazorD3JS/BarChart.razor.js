export function createBarChart(element, data) {
    // Select the element and clear previous chart content
    const svgContainer = d3.select(`#${element}`);
    svgContainer.selectAll("*").remove();

    // Set dimensions and margins for the bar chart
    const margin = { top: 20, right: 20, bottom: 50, left: 50 };
    const width = 400 - margin.left - margin.right;
    const height = 400 - margin.top - margin.bottom;

    // Create SVG container
    const svg = svgContainer.append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform", `translate(${margin.left}, ${margin.top})`);

    // X scale - maps Name categories to the width of the chart
    const x = d3.scaleBand()
        .domain(data.map(d => d.name))
        .range([0, width])
        .padding(0.1); // Space between bars

    // Y scale - maps Values to the height of the chart
    const y = d3.scaleLinear()
        .domain([0, d3.max(data, d => d.value)])
        .range([height, 0]);

    // Append X axis
    svg.append("g")
        .attr("transform", `translate(0, ${height})`)
        .call(d3.axisBottom(x))
        .selectAll("text")
        .attr("transform", "rotate(-45)")
        .style("text-anchor", "end");

    // Append Y axis
    svg.append("g")
        .call(d3.axisLeft(y));

    // Add bars to the chart
    svg.selectAll(".bar")
        .data(data)
        .enter()
        .append("rect")
        .attr("class", "bar")
        .attr("x", d => x(d.name))
        .attr("y", d => y(d.value))
        .attr("width", x.bandwidth())
        .attr("height", d => height - y(d.value))
        .attr("fill", "#69b3a2"); // You can customize this color

    console.log("Bar chart rendering complete.");
}
