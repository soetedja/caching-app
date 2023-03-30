export class CacheInfo {
    totalHits: number = 0;
    totalMisses: number = 0;
    memoryLayout: number[] = [];
    hitFrequency: { [key: string]: number } = {};
    // cacheAuditLogs: { [key: number]: Date } = {};
}
