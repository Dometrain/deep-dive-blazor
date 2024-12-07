export function getGreeting(name) {
    return `Hello, ${name}! Welcome to Blazor with JavaScript!`;
}

export function getTimezone () {
    return Intl.DateTimeFormat().resolvedOptions().timeZone;
}

export async function setupShortcuts() {
    const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
    var exports = await getAssemblyExports("JavaScriptDemo.dll");
    console.log(exports);
    document.addEventListener("keydown", (event) => {
        event.preventDefault();
        if (event.ctrlKey && event.shiftKey && event.key.toLowerCase() === 's') {
            exports.JavaScriptDemo.Services.InteropService.TriggerShortcut(event.key);
        }
    });

}