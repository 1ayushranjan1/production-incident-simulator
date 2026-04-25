import React from "react";
import Dashboard from "./components/Dashboard";
import IncidentTable from "./components/IncidentTable";
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
    return (
        <>
            <Dashboard />
            <IncidentTable />
        </>
    );
}

export default App;