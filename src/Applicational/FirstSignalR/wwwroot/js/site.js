// Write your Javascript code.

var conn = new signalR.HubConnectionBuilder()
    .withUrl("/appEvents")
    .configureLogging(signalR.LogLevel.Trace)
    .build();

conn.on("ReceiveEvent", (appEvent) =>
{
    console.log(appEvent);

    const li = document.createElement("li");
    li.textContent = JSON.stringify(appEvent);
    document.getElementById("processEvents").appendChild(li);


});

document.getElementById("askForEvents").addEventListener("click", event => {
    const processId = document.getElementById("processId").value;
    if (processId === undefined || processId === null)
        processId = -1;

    conn.invoke("StartProcessMonitor", processId).catch(err => console.error(err.toString()));
    event.preventDefault();
});

conn.start().catch(err => console.error(err.toString()));