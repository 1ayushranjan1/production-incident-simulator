import React, { useEffect, useState } from "react";
import { getStats } from "../services/api";

const Dashboard = () => {
    const [stats, setStats] = useState({
        totalOrders: 0,
        failed: 0,
        resolved: 0
    });

    useEffect(() => {
        loadStats();
    }, []);

    const loadStats = () => {
        getStats().then(res => setStats(res.data));
    };

    return (
        <div className="container mt-4">
            <h2>Production Incident Dashboard</h2>

            <div className="row mt-3">
                <div className="col-md-4">
                    <div className="card p-3 bg-light">
                        <h5>Total Orders</h5>
                        <h3>{stats.totalOrders}</h3>
                    </div>
                </div>

                <div className="col-md-4">
                    <div className="card p-3 bg-danger text-white">
                        <h5>Failed</h5>
                        <h3>{stats.failed}</h3>
                    </div>
                </div>

                <div className="col-md-4">
                    <div className="card p-3 bg-success text-white">
                        <h5>Resolved</h5>
                        <h3>{stats.resolved}</h3>
                    </div>
                </div>
            </div>
        </div>
        
    );
};

export default Dashboard;