export function createPieChart(element, data,width,height) {
    console.log(data);
    // Select the element where we want to append the pie chart
    const svgContainer = d3.select(`#${element}`);
    svgContainer.selectAll("*").remove(); // Clear previous chart if any
    const radius = Math.min(width, height) / 2;

    const svg = svgContainer.append("svg")
        .attr("width", width)
        .attr("height", height)
        .append("g")
        .attr("transform", `translate(${width / 2}, ${height / 2})`);

    // Define color scale
    const color = d3.scaleOrdinal()
        .domain(data.map(d => d.name))
        .range(d3.schemeCategory10);

    // Create pie generator
    const pie = d3.pie()
        .value(d => d.value)
        .sort(null);

    // Create arc generator
    const arc = d3.arc()
        .innerRadius(0)
        .outerRadius(radius);

    // Generate pie chart with data
    const arcs = svg.selectAll("arc")
        .data(pie(data))
        .enter()
        .append("g")
        .attr("class", "arc");

    // Append the arcs and fill with colors
    arcs.append("path")
        .attr("d", arc)
        .attr("fill", d => color(d.data.name));

    // Add labels
    arcs.append("text")
        .attr("transform", d => `translate(${arc.centroid(d)})`)
        .attr("text-anchor", "middle")
        .text(d => d.data.name);

    console.log("Pie chart rendering complete.");
}

export function createPieChartWithAnimation(element, data) {
    // Select the element and clear previous chart content
    const svgContainer = d3.select(`#${element}`);
    svgContainer.selectAll("*").remove();

    // Set width, height, and radius
    const width = 400;
    const height = 400;
    const radius = Math.min(width, height) / 2;

    const svg = svgContainer.append("svg")
        .attr("width", width)
        .attr("height", height)
        .append("g")
        .attr("transform", `translate(${width / 2}, ${height / 2})`);

    // Color scale
    const color = d3.scaleOrdinal()
        .domain(data.map(d => d.name))
        .range(d3.schemeCategory10);

    // Pie and arc generators
    const pie = d3.pie()
        .value(d => parseInt(d.value))
        .sort(null);

    const arc = d3.arc()
        .innerRadius(0)
        .outerRadius(radius);

    // Bind data to arcs
    const arcs = svg.selectAll(".arc")
        .data(pie(data), d => d.data.name);

    // Enter new arcs
    const enterArcs = arcs.enter()
        .append("g")
        .attr("class", "arc");

    // Append paths and apply initial transition
    enterArcs.append("path")
        .attr("fill", d => color(d.data.name))
        .attr("stroke", "white")
        .style("stroke-width", "2px")
        .transition()  // Transition on enter
        .duration(750)
        .attrTween("d", function (d) {
            const interpolate = d3.interpolate({ startAngle: 0, endAngle: 0 }, d);
            return t => arc(interpolate(t));
        });

    // Append text labels with initial positions
    enterArcs.append("text")
        .attr("text-anchor", "middle")
        .attr("font-size", "14px")
        .transition()
        .duration(750)
        .attr("transform", d => `translate(${arc.centroid(d)})`)
        .text(d => d.data.name);

    // Update existing arcs with new data
    arcs.select("path")
        .transition()
        .duration(750)
        .attrTween("d", function (d) {
            const interpolate = d3.interpolate(this._current, d);
            this._current = interpolate(1);
            return t => arc(interpolate(t));
        });

    arcs.select("text")
        .transition()
        .duration(750)
        .attr("transform", d => `translate(${arc.centroid(d)})`)
        .text(d => d.data.Name);

    // Store the current angles for transition on next update
    arcs.select("path").each(function (d) { this._current = d; });
}
