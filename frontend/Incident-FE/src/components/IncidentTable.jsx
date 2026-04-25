import React, { useEffect, useState } from "react";
import { getIncidents, retryIncident } from "../services/api";

const IncidentTable = () => {
    const [incidents, setIncidents] = useState([]);

    useEffect(() => {
        loadData();
    }, []);

    const loadData = () => {
        getIncidents().then(res => setIncidents(res.data));
    };

    const handleRetry = async (id) => {
        try {
            await retryIncident(id);
            alert("Retry successful");
            loadData();
        } catch {
            alert("Retry failed");
        }
    };

    return (
        <div className="container mt-4">
            <h3>Incident Logs</h3>

            <table className="table table-bordered mt-3">
                <thead className="table-dark">
                    <tr>
                        <th>ID</th>
                        <th>Error</th>
                        <th>Status</th>
                        <th>Action</th>
                    </tr>
                </thead>

                <tbody>
                    {incidents.map(i => (
                        <tr key={i.incidentId}>
                            <td>{i.incidentId}</td>
                            <td>{i.errorMessage}</td>
                            <td>
                                <span className={
                                    i.status === "Failed"
                                        ? "text-danger"
                                        : "text-success"
                                }>
                                    {i.status}
                                </span>
                            </td>
                            <td>
                                {i.status === "Failed" && (
                                    <button
                                        className="btn btn-warning"
                                        onClick={() => handleRetry(i.incidentId)}
                                    >
                                        Retry
                                    </button>
                                )}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default IncidentTable;