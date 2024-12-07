export function beforeWebStart(options) {
    console.log("beforeWebStart triggered")
}

export function afterWebStarted(options) {
    console.log("afterWebStarted triggered")
}


export function beforeServerStart(options, extensions) {
    console.log("beforeServerStart triggered")
}

export function afterServerStarted(options) {
    console.log("afterServerStarted triggered")
}

export function beforeWebAssemblyStart(options, extensions) {
    console.log("beforeWebAssemblyStart triggered")
}
export function afterWebAssemblyStarted(blazor) {
    console.log("afterWebAssemblyStarted triggered")
}

//Special for Wasm
export function onRuntimeConfigLoaded(config) {
    console.log("onRuntimeConfigLoaded triggered")
}

export function onRuntimeReady({ getAssemblyExports, getConfig }) {
    console.log("onRuntimeReady triggered")
}
